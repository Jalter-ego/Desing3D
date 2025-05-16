using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Diseño3D
{
    public class Poligono: ISerializable,IterableObject
    {
        public List<Vector> listaDeVertices;
        public Color4 color;
        public Vector centro;
        private Vector3 Traslacion { get; set; } = Vector3.Zero;
        private Vector Escala { get; set; } = new Vector(1,1,1);
        private float RotacionY { get; set; } = 0f;
        private float RotacionX { get; set; } = 0f;
        private float RotacionZ { get; set; } = 0f;



        public Poligono(Color4 color)
        {
            this.listaDeVertices = new List<Vector>();
            this.color = color;
            this.centro = new Vector(0, 0, 0);
        }

        public void SetColor(Color4 color)
        {
            this.color = color;
        }

        public void SetCentro(Vector centro)
        {
            this.centro = centro;
        }

        public List<Vector> GetVertices()
        {
            return this.listaDeVertices;
        }

        public void Add(Vector v)
        {
            this.listaDeVertices.Add(v);
            //this.centro = this.CalcularCentroMasa();
        }

        public void Draw()
        {
            GL.PushMatrix();
            GL.Translate(Traslacion);
            GL.Rotate(RotacionX, 1.0f, 0.0f, 0.0f);
            GL.Rotate(RotacionY, 0.0f, 1.0f, 0.0f);
            GL.Rotate(RotacionZ, 0.0f, 0.0f, 1.0f);
            GL.Scale(Escala.VectorAVector3());


            // 3. Dibujar en torno al origen
            GL.Color4(color);
            GL.Begin(PrimitiveType.Quads);

            foreach (Vector vector in listaDeVertices)
            {
                GL.Vertex3((vector+centro).VectorAVector3());
            }
            GL.End();
            GL.PopMatrix();
        }

        public void Draw3()
        {
            GL.PushMatrix();
            GL.PushAttrib(AttribMask.TextureBit | AttribMask.EnableBit | AttribMask.LightingBit); // Guarda estados relacionados
            //GL.Enable(EnableCap.Lighting); // Habilita la iluminación
            GL.Enable(EnableCap.Light0);   // Habilita la luz 0
            GL.Light(LightName.Light0, LightParameter.Position, new float[] { 5.0f, 5.0f, 5.0f, 0.0f }); // Luz direccional
            GL.Light(LightName.Light0, LightParameter.Ambient, new float[] { 0.2f, 0.2f, 0.2f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.Diffuse, new float[] { 0.8f, 0.8f, 0.8f, 1.0f });
            GL.Light(LightName.Light0, LightParameter.Specular, new float[] { 0.5f, 0.5f, 0.5f, 1.0f });


            GL.Color4(color);
            GL.Begin(PrimitiveType.Triangles);
            foreach (Vector vector in listaDeVertices)
            {
                // Asegúrate que aquí ya no sumas '+ centro' si seguiste la recomendación anterior
                GL.Vertex3(vector.VectorAVector3());
            }
            GL.End();
            GL.PopMatrix();
        }


        public void Rotar(float angulo, Vector3 eje)
        {
            if (eje.X != 0) RotacionX += angulo;
            if (eje.Y != 0) RotacionY += angulo;
            if (eje.Z != 0) RotacionZ += angulo;
        }

        public void Trasladar(Vector3 delta)
        {
            for (int i = 0; i < listaDeVertices.Count; i++)
            {
                listaDeVertices[i].x += delta.X;
                listaDeVertices[i].y += delta.Y;
                listaDeVertices[i].z += delta.Z;
            }
            // El centro del polígono también se mueve
            this.centro.x += delta.X;
            this.centro.y += delta.Y;
            this.centro.z += delta.Z;
            // Opcionalmente, recalcula el centro exacto si es necesario
            // this.centro = this.CalcularCentroMasa();
        }

        public void Rotar(float angulo, Vector3 eje, Vector puntoFijo)
        {
            Matrix4 trans1 = Matrix4.CreateTranslation(-puntoFijo.x, -puntoFijo.y, -puntoFijo.z);
            Matrix4 rot = Matrix4.CreateFromAxisAngle(eje, MathHelper.DegreesToRadians(angulo));
            Matrix4 trans2 = Matrix4.CreateTranslation(puntoFijo.x, puntoFijo.y, puntoFijo.z);

            Matrix4 transformacion = trans1 * rot * trans2;

            for (int i = 0; i < listaDeVertices.Count; i++)
            {
                Vector3 v = listaDeVertices[i].VectorAVector3();
                Vector4 v4 = new Vector4(v, 1.0f);
                Vector4 resultado = Vector4.Transform(v4, transformacion);

                listaDeVertices[i] = new Vector(resultado.X, resultado.Y, resultado.Z);
            }
        }


        public void Escalar(float escalar, Vector3 factor)
        {
            Escala.x *= (factor.X > 0) ? factor.X + escalar : 1;
            Escala.y *= (factor.Y > 0) ? factor.Y + escalar : 1;
            Escala.z *= (factor.Z > 0) ? factor.Z + escalar : 1;
        }


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        private Vector MinVertice()
        {
            Vector minV = new Vector(listaDeVertices[0].x, listaDeVertices[0].y, listaDeVertices[0].z);
            foreach (var vertice in listaDeVertices)
            {
                if (vertice.x < minV.x)
                    minV.x = vertice.x;
                if (vertice.y < minV.y)
                    minV.y = vertice.y;
                if (vertice.z < minV.z)
                    minV.z = vertice.z;
            }
            return minV;
        }

        private Vector MaxVertice()
        {
            Vector maxV = new Vector(listaDeVertices[0].x, listaDeVertices[0].y, listaDeVertices[0].z);
            foreach (var vertice in listaDeVertices)
            {
                if (vertice.x > maxV.x)
                    maxV.x = vertice.x;
                if (vertice.y > maxV.y)
                    maxV.y = vertice.y;
                if (vertice.z > maxV.z)
                    maxV.z = vertice.z;
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

        Vector IterableObject.GetCentro()
        {
            throw new NotImplementedException();
        }
    }
}
