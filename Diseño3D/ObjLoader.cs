using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Diseño3D
{
    public class ObjLoader : IDisposable
    {
        // Almacenamiento temporal durante la carga
        public List<Vector3> _vertices = new List<Vector3>();
        private List<Vector2> _texCoords = new List<Vector2>();
        private List<Vector3> _normals = new List<Vector3>();

        // Datos organizados por material para el renderizado
        public Dictionary<string, Material> _materials = new Dictionary<string, Material>();
        public Dictionary<string, List<Tuple<int, int, int>>> _facesByMaterial = new Dictionary<string, List<Tuple<int, int, int>>>();

        private string _objFilePath;
        private string _mtlFilePath;
        private string _basePath;
        private float RotacionY { get; set; } = 0f;
        private float RotacionX { get; set; } = 0f;
        private float RotacionZ { get; set; } = 0f;
        private Vector3 Traslacion { get; set; } = Vector3.Zero;
        private Vector Escala { get; set; } = new Vector(1, 1, 1);

        private Vector3 centro;


        public ObjLoader(string objPath)
        {
            _objFilePath = objPath;
            _basePath = Path.GetDirectoryName(objPath);

            if (!File.Exists(_objFilePath))
            {
                throw new FileNotFoundException($"El archivo OBJ no se encontró en: '{_objFilePath}'");
            }
            this.centro = new Vector3(0, 0, 0);

            LoadObjFile();
        }

        private void LoadObjFile()
        {
            string currentMaterial = null; // Material activo actual

            try
            {
                foreach (string line in File.ReadLines(_objFilePath))
                {
                    string[] parts = line.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 0) continue;

                    string keyword = parts[0];

                    switch (keyword)
                    {
                        case "mtllib": // Referencia al archivo de materiales
                            if (parts.Length > 1)
                            {
                                _mtlFilePath = Path.Combine(_basePath, parts[1]);
                                LoadMaterials(_mtlFilePath);
                            }
                            break;

                        case "usemtl": // Cambia el material activo
                            if (parts.Length > 1)
                            {
                                currentMaterial = parts[1];
                                if (!_facesByMaterial.ContainsKey(currentMaterial))
                                {
                                    _facesByMaterial[currentMaterial] = new List<Tuple<int, int, int>>();

                                    if (!_materials.ContainsKey(currentMaterial))
                                    {
                                        Console.WriteLine($"Advertencia: El material '{currentMaterial}' se usó en .obj pero no se definió en .mtl o el archivo .mtl no se cargó.");
                                        _materials[currentMaterial] = new Material(currentMaterial); // Material por defecto
                                    }
                                }
                            }
                            break;

                        case "v": // Vértice geométrico
                            if (parts.Length > 3)
                            {
                                _vertices.Add(new Vector3(
                                    ParseFloat(parts[1]),
                                    ParseFloat(parts[2]),
                                    ParseFloat(parts[3])));
                            }
                            break;

                        case "vt": // Coordenada de textura
                            if (parts.Length > 2)
                            {
                                // A menudo, las coordenadas V en OBJ están invertidas respecto a OpenGL
                                // (OpenGL 0.0 abajo, OBJ 0.0 arriba). Se corrige restando de 1.0f.
                                _texCoords.Add(new Vector2(
                                    ParseFloat(parts[1]),
                                    1.0f - ParseFloat(parts[2]))); // Corrección de V
                            }
                            else if (parts.Length > 1) // Manejar caso con solo U
                            {
                                _texCoords.Add(new Vector2(ParseFloat(parts[1]), 0.0f));
                            }
                            break;

                        case "vn": // Normal de vértice
                            if (parts.Length > 3)
                            {
                                _normals.Add(new Vector3(
                                    ParseFloat(parts[1]),
                                    ParseFloat(parts[2]),
                                    ParseFloat(parts[3])));
                            }
                            break;

                        case "f": // Cara (face)
                            if (parts.Length > 3 && currentMaterial != null) // Necesita al menos 3 vértices y un material activo
                            {
                                // Asumimos triangulación si hay más de 3 vértices (polígonos)
                                // Creamos un abanico de triángulos (triangle fan) desde el primer vértice
                                for (int i = 2; i < parts.Length - 1; i++)
                                {
                                    ParseFaceVertex(parts[1], currentMaterial);
                                    ParseFaceVertex(parts[i], currentMaterial);
                                    ParseFaceVertex(parts[i + 1], currentMaterial);
                                }
                            }
                            break;

                        default:
                            // Ignorar otros keywords o comentarios (#)
                            break;
                    }
                }
                Console.WriteLine($"Archivo OBJ '{Path.GetFileName(_objFilePath)}' cargado:");
                Console.WriteLine($"- Vértices: {_vertices.Count}");
                Console.WriteLine($"- Coordenadas Textura: {_texCoords.Count}");
                Console.WriteLine($"- Normales: {_normals.Count}");
                Console.WriteLine($"- Materiales Cargados: {_materials.Count}");
                Console.WriteLine($"- Grupos de Caras (por material): {_facesByMaterial.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer el archivo OBJ '{_objFilePath}': {ex.Message}");
                // Podrías lanzar la excepción de nuevo o manejarla de otra forma
                throw;
            }
        }

        // Parsea un vértice de una definición de cara (ej: "v/vt/vn")
        private void ParseFaceVertex(string facePart, string material)
        {
            // Formatos posibles: v, v/vt, v//vn, v/vt/vn
            string[] indices = facePart.Split('/');
            int vertexIndex = 0, texCoordIndex = 0, normalIndex = 0;

            // Índice del vértice (siempre presente)
            if (indices.Length > 0 && int.TryParse(indices[0], out int vIdx))
            {
                vertexIndex = vIdx;
            }

            // Índice de coordenada de textura (si existe)
            if (indices.Length > 1 && int.TryParse(indices[1], out int vtIdx))
            {
                texCoordIndex = vtIdx;
            }

            // Índice de la normal (si existe)
            if (indices.Length > 2 && int.TryParse(indices[2], out int vnIdx))
            {
                normalIndex = vnIdx;
            }

            // Los índices en OBJ son 1-based, los convertimos a 0-based para listas C#
            // Si un índice es 0 significa que no estaba presente en el archivo
            _facesByMaterial[material].Add(Tuple.Create(
                vertexIndex > 0 ? vertexIndex - 1 : -1,
                texCoordIndex > 0 ? texCoordIndex - 1 : -1,
                normalIndex > 0 ? normalIndex - 1 : -1
            ));
        }

        private void LoadMaterials(string mtlPath)
        {
            if (!File.Exists(mtlPath))
            {
                Console.WriteLine($"Advertencia: Archivo MTL no encontrado en '{mtlPath}'");
                return;
            }

            string mtlBasePath = Path.GetDirectoryName(mtlPath); // Directorio del archivo .mtl
            Material currentMaterial = null;

            try
            {
                foreach (string line in File.ReadLines(mtlPath))
                {
                    string[] parts = line.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 0) continue;

                    string keyword = parts[0];

                    switch (keyword)
                    {
                        // Dentro de LoadMaterials, después de crear el Material:
                        case "newmtl":
                            if (parts.Length > 1)
                            {
                                string materialName = parts[1];
                                currentMaterial = new Material(materialName);
                                _materials[materialName] = currentMaterial;
                                Console.WriteLine($"--> Material encontrado en .mtl: {materialName}"); // <-- Añade esto
                            }
                            break;

                        case "Ka": // Color Ambiental
                            if (currentMaterial != null && parts.Length > 3)
                            {
                                currentMaterial.AmbientColor = new Color4(ParseFloat(parts[1]), ParseFloat(parts[2]), ParseFloat(parts[3]), 1.0f);
                            }
                            break;

                        case "Kd": // Color Difuso
                            if (currentMaterial != null && parts.Length > 3)
                            {
                                currentMaterial.DiffuseColor = new Color4(ParseFloat(parts[1]), ParseFloat(parts[2]), ParseFloat(parts[3]), 1.0f);
                            }
                            break;

                        case "Ks": // Color Especular
                            if (currentMaterial != null && parts.Length > 3)
                            {
                                currentMaterial.SpecularColor = new Color4(ParseFloat(parts[1]), ParseFloat(parts[2]), ParseFloat(parts[3]), 1.0f);
                            }
                            break;

                        case "Ns": // Exponente especular (Shininess)
                            if (currentMaterial != null && parts.Length > 1)
                            {
                                currentMaterial.Shininess = ParseFloat(parts[1]);
                            }
                            break;

                        case "d": // Disolver (Transparencia) - Alpha en Diffuse
                            if (currentMaterial != null && parts.Length > 1)
                            {
                                float alpha = ParseFloat(parts[1]);
                                currentMaterial.DiffuseColor = new Color4(
                                    currentMaterial.DiffuseColor.R,
                                    currentMaterial.DiffuseColor.G,
                                    currentMaterial.DiffuseColor.B,
                                    alpha);
                                // Podrías querer aplicarlo también a Ambient y Specular si usas transparencia
                            }
                            break;

                        case "Tr": // Transparencia (1 - d) - Alternativa a 'd'
                            if (currentMaterial != null && parts.Length > 1)
                            {
                                float transparency = ParseFloat(parts[1]);
                                float alpha = 1.0f - transparency;
                                currentMaterial.DiffuseColor = new Color4(
                                    currentMaterial.DiffuseColor.R,
                                    currentMaterial.DiffuseColor.G,
                                    currentMaterial.DiffuseColor.B,
                                    alpha);
                            }
                            break;

                        case "map_Kd": // Mapa de textura difusa
                            if (currentMaterial != null && parts.Length > 1)
                            {
                                // El resto de la línea puede contener opciones, tomamos la última parte como nombre de archivo
                                string textureFileName = parts.LastOrDefault();
                                if (!string.IsNullOrEmpty(textureFileName))
                                {
                                    currentMaterial.SetTexture(textureFileName, mtlBasePath);
                                }
                            }
                            break;

                        // Puedes añadir aquí el manejo de otros mapas como map_Ks, map_Ka, bump, etc.

                        default:
                            // Ignorar otros keywords o comentarios (#)
                            break;
                    }
                }
                Console.WriteLine($"Archivo MTL '{Path.GetFileName(mtlPath)}' cargado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer el archivo MTL '{mtlPath}': {ex.Message}");
                // No relanzamos, la carga del OBJ puede continuar sin materiales definidos.
            }
        }

        // Dibuja el modelo usando OpenGL (modo inmediato)
        public void Draw()
        {
            GL.PushAttrib(AttribMask.TextureBit | AttribMask.EnableBit | AttribMask.LightingBit); // Guarda estados relacionados
            GL.Enable(EnableCap.Lighting); // Habilita la iluminación
            GL.Enable(EnableCap.Light0);   // Habilita la luz 0
            // Configura la luz (posición, colores) - Ejemplo básico
            GL.Light(LightName.Light0, LightParameter.Position, new float[] { 5.0f, 5.0f, 5.0f, 0.0f }); // Luz direccional
            GL.Light(LightName.Light0, LightParameter.Ambient, new float[] { 0.2f, 0.2f, 0.2f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.Diffuse, new float[] { 0.8f, 0.8f, 0.8f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.Specular, new float[] { 0.5f, 0.5f, 0.5f, 1.0f });
            GL.Rotate(RotacionX, 1.0f, 0.0f, 0.0f);
            GL.Rotate(RotacionY, 0.0f, 1.0f, 0.0f);
            GL.Rotate(RotacionZ, 0.0f, 0.0f, 1.0f);
            GL.Translate(Escala.VectorAVector3());
            GL.Translate(Traslacion);
            foreach (var kvp in _facesByMaterial)
            {
                string materialName = kvp.Key;
                List<Tuple<int, int, int>> faces = kvp.Value;
                if (materialName == "Wheels" || materialName == "Tires")
                {

                    // Busca y aplica el material correspondiente
                    if (_materials.TryGetValue(materialName, out Material material))
                    {
                        material.Apply(); 
                    }
                    else
                    {
                        GL.Disable(EnableCap.Texture2D);
                        GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, Color4.Gray);
                        GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, Color4.LightGray);
                        GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, Color4.Black);
                        GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, 1.0f);
                        Console.WriteLine($"Advertencia: Dibujando con material por defecto para '{materialName}' porque no se encontró.");
                    }
                    GL.Begin(PrimitiveType.Triangles);
                    
                    foreach (var faceVertexIndices in faces)
                    {
                        int vIdx = faceVertexIndices.Item1;
                        if (vIdx >= 0 && vIdx < _vertices.Count)
                        {
                            GL.Vertex3(_vertices[vIdx] + centro);
                        }
                        else
                        {
                            Console.WriteLine($"Advertencia: Índice de vértice inválido ({vIdx}) encontrado en material '{materialName}'.");
                        }
                    }
                    GL.End();
                }
            }

            GL.PopAttrib(); // Restaura los estados de OpenGL guardados
            GL.BindTexture(TextureTarget.Texture2D, 0); // Asegura desvincular textura al final
            GL.Disable(EnableCap.Texture2D); // Asegura deshabilitar texturizado
        }


        public List<Vector3> prueba()
        {
            List<Vector3> ruedas = new List<Vector3>();
            foreach (var kvp in _facesByMaterial)
            {
                string materialName = kvp.Key;
                if (materialName != "Wheels" && materialName != "Tires")
                {
                    Console.WriteLine(materialName);

                    List<Tuple<int, int, int>> faces = kvp.Value;
                    foreach (var faceVertexIndices in faces)
                    {
                        int vIdx = faceVertexIndices.Item1;
                        if (vIdx >= 0 && vIdx < _vertices.Count)
                        {
                            //Console.WriteLine("x: " + _vertices[vIdx].X + " Y: " + _vertices[vIdx].Y + " Z: " + _vertices[vIdx].Z);
                            //ruedas.Add(_vertices[vIdx]);
                        }
                        else
                        {
                            Console.WriteLine($"Advertencia: Índice de vértice inválido ({vIdx}) encontrado en material '{materialName}'.");
                        }
                    }
                }
            }
            return ruedas;
        }

        // Función auxiliar para parsear floats asegurando la cultura invariante (punto decimal)
        private float ParseFloat(string value)
        {
            return float.Parse(value, CultureInfo.InvariantCulture);
        }

        // Implementación de IDisposable para liberar recursos de OpenGL (texturas)
        public void Dispose()
        {
            foreach (var material in _materials.Values)
            {
                material.Dispose(); // Llama al Dispose de cada material para liberar su textura
            }
            _materials.Clear();
            _facesByMaterial.Clear();
            _vertices.Clear();
            _texCoords.Clear();
            _normals.Clear();
            // GC.SuppressFinalize(this); // Descomentar si añades un finalizador (~ObjLoader())
        }

        public void SetCentro(Vector3 v)
        {
            this.centro = v;
        }



        public void Rotar(float angulo, Vector3 eje)
        {
            if (eje.X != 0) RotacionX += angulo;
            if (eje.Y != 0) RotacionY += angulo;
            if (eje.Z != 0) RotacionZ += angulo;
        }

        public void Rotar(float angulo, Vector3 eje, Vector puntoFijo)
        {
            Matrix4 trans1 = Matrix4.CreateTranslation(-puntoFijo.x, -puntoFijo.y, -puntoFijo.z);
            Matrix4 rot = Matrix4.CreateFromAxisAngle(eje, MathHelper.DegreesToRadians(angulo));
            Matrix4 trans2 = Matrix4.CreateTranslation(puntoFijo.x, puntoFijo.y, puntoFijo.z);

            Matrix4 transformacion = trans1 * rot * trans2;

            for (int i = 0; i < _vertices.Count; i++)
            {
                Vector3 v = _vertices[i];
                Vector4 v4 = new Vector4(v, 1.0f);
                Vector4 resultado = Vector4.Transform(v4, transformacion);

                _vertices[i] = new Vector3(resultado.X, resultado.Y, resultado.Z);
            }
        }

        private Vector MinVertice()
        {
            Vector minV = new Vector(_vertices[0].X, _vertices[0].Y, _vertices[0].Z);
            foreach (var vertice in _vertices)
            {
                if (vertice.X < minV.x)
                    minV.x = vertice.X;
                if (vertice.Y < minV.y)
                    minV.y = vertice.Y;
                if (vertice.Z < minV.z)
                    minV.z = vertice.Z;
            }
            return minV;
        }

        private Vector MaxVertice()
        {
            Vector maxV = new Vector(_vertices[0].X, _vertices[0].Y, _vertices[0].Z);
            foreach (var vertice in _vertices)
            {
                if (vertice.X > maxV.x)
                    maxV.x = vertice.X;
                if (vertice.Y > maxV.y)
                    maxV.y = vertice.Y;
                if (vertice.Z > maxV.z)
                    maxV.z = vertice.Z;
            }
            return maxV;
        }

        public Vector CalcularCentroMasa()
        {
            Vector menoresV = MinVertice();
            Vector mayoresV = MaxVertice();
            Vector nuevoCentro = new Vector((menoresV.x + mayoresV.x) / 2, (menoresV.y + mayoresV.y) / 2, (menoresV.z + mayoresV.z) / 2);
            return nuevoCentro;
        }

        public void Trasladar(Vector3 delta)
        {
            Traslacion += delta;
        }

        public void Escalar(float escalar, Vector3 factor)
        {
            Escala.x *= (factor.X > 0) ? factor.X + escalar : 1;
            Escala.y *= (factor.Y > 0) ? factor.Y + escalar : 1;
            Escala.z *= (factor.Z > 0) ? factor.Z + escalar : 1;
        }


    }
}