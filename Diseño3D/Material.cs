using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Diseño3D
{
    public class Material
    {
        public string Name { get; private set; }
        public Color4 AmbientColor { get; set; } = Color4.Gray; // Ka
        public Color4 DiffuseColor { get; set; } = Color4.White; // Kd
        public Color4 SpecularColor { get; set; } = Color4.Black; // Ks
        public float Shininess { get; set; } = 1.0f; // Ns
        public int TextureId { get; private set; } = 0; // map_Kd
        public string TexturePath { get; private set; } = null;

        // Constructor simple
        public Material(string name)
        {
            Name = name;
            TextureId = 0; // Sin textura por defecto
        }

        // Método para establecer la ruta de la textura y cargarla
        public void SetTexture(string texturePath, string basePath)
        {
            // Combinar la ruta base (del archivo .mtl) con la ruta relativa de la textura
            string fullPath = Path.Combine(basePath, texturePath);

            if (File.Exists(fullPath))
            {
                TexturePath = fullPath;
                TextureId = LoadTexture(fullPath);
                if (TextureId == 0)
                {
                    Console.WriteLine($"Advertencia: No se pudo cargar la textura '{fullPath}' para el material '{Name}'.");
                }
            }
            else
            {
                Console.WriteLine($"Advertencia: Archivo de textura no encontrado '{fullPath}' para el material '{Name}'.");
                TexturePath = null;
                TextureId = 0;
            }
        }

        // Carga una textura desde un archivo y devuelve su ID de OpenGL
        private int LoadTexture(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"Error: El archivo de textura no existe en '{path}'");
                return 0; // Retorna 0 si el archivo no existe
            }

            try
            {
                int textureId = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, textureId);

                // Usamos System.Drawing.Bitmap para cargar la imagen
                using (Bitmap bitmap = new Bitmap(path))
                {
                    // Los archivos BMP a menudo se cargan al revés verticalmente,
                    // pero las texturas OBJ a menudo ya están orientadas correctamente para OpenGL.
                    // Si la textura aparece invertida, descomenta la siguiente línea:
                    // bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);

                    BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                        ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb); // Usa ARGB

                    // Sube los datos de la textura a OpenGL
                    // Nota: El formato de Bitmap es BGRA, pero usamos PixelFormat.Format32bppArgb.
                    // OpenGL lo interpretará correctamente si especificamos Bgra.
                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                        OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

                    bitmap.UnlockBits(data);
                }

                // Configura parámetros de la textura (filtrado y envoltura)
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

                // Genera mipmaps para mejorar la calidad y el rendimiento
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

                GL.BindTexture(TextureTarget.Texture2D, 0); // Desvincula la textura

                return textureId;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar la textura '{path}': {ex.Message}");
                return 0; // Retorna 0 en caso de error
            }
        }

        // Aplica las propiedades del material (colores y textura) en OpenGL
        public void Apply()
        {
            // Establece las propiedades del material para la iluminación
            // Asegúrate de que la iluminación esté habilitada (GL.Enable(EnableCap.Lighting))
            // y configurada en tu ciclo principal de renderizado para que esto tenga efecto.
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, AmbientColor);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, DiffuseColor);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, SpecularColor);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, Shininess);

            // Aplica la textura si existe
            if (TextureId > 0)
            {
                GL.Enable(EnableCap.Texture2D); // Habilita el texturizado 2D
                GL.BindTexture(TextureTarget.Texture2D, TextureId); // Vincula la textura del material
            }
            else
            {
                GL.Disable(EnableCap.Texture2D); // Deshabilita el texturizado si no hay textura
                GL.BindTexture(TextureTarget.Texture2D, 0); // Desvincula cualquier textura activa
            }
        }

        // Método para liberar recursos de OpenGL (la textura)
        public void Dispose()
        {
            if (TextureId > 0)
            {
                GL.DeleteTexture(TextureId);
                TextureId = 0;
            }
        }
    }
}