using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Diseño3D
{
    public class Poligono: ISerializable
    {
        public List<Vector> listaDeVertices;
        public Color4 color;
        public Vector centro;

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
            //1.0f, 0.3f, 0.3f
            GL.Color4(color);
            GL.Begin(PrimitiveType.Quads);

            foreach (Vector vector in this.listaDeVertices)
            {
                GL.Vertex3(vector.x + centro.x, vector.y + centro.y, vector.z + centro.z);
            }

            GL.End();
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

        public void Rotar(float angulo, Vector3 eje)
        {
            if (!eje.Equals(new Vector3(0.0f, 0.0f, 0.0f)))
            {
                Matrix4 rotacion = Matrix4.CreateFromAxisAngle(eje, MathHelper.DegreesToRadians(angulo));
                for (int i = 0; i < listaDeVertices.Count; i++)
                {
                    if (centro.VectorAVector3().Equals(new Vector3(0.0f, 0.0f, 0.0f)))
                    {
                        Vector3 vector = Vector3.TransformPosition(listaDeVertices[i].VectorAVector3(), rotacion);
                        listaDeVertices[i] = (Vector)vector;
                    }
                    else
                    {
                        Vector3 verticeEnOrigen = listaDeVertices[i].VectorAVector3() - centro.VectorAVector3();
                        Vector3 vector = Vector3.TransformPosition(verticeEnOrigen, rotacion);
                        listaDeVertices[i] = (Vector)vector + centro;
                    }
                }
            }
        }

        public void Trasladar(Vector3 otroCentro)
        {
            Matrix4 traslacion = Matrix4.CreateTranslation(otroCentro);
            for (int i = 0; i < listaDeVertices.Count; i++)
            {
                Vector3 vector = Vector3.TransformPosition(listaDeVertices[i].VectorAVector3(), traslacion);
                listaDeVertices[i] = (Vector)vector;
            }
            centro = CalcularCentroMasa();
        }

        public void Escalar(float escalar, Vector3 factor)
        {
            if (factor.X > 0)
                factor.X += escalar;
            else
                factor.X = 1;
            if (factor.Y > 0)
                factor.Y += escalar;
            else
                factor.Y = 1;
            if (factor.Z > 0)
                factor.Z += escalar;
            else
                factor.Z = 1;
            Matrix4 escalacion = Matrix4.CreateScale(factor);

            for (int i = 0; i < listaDeVertices.Count; i++)
            {
                if (centro.VectorAVector3().Equals(new Vector3(0.0f, 0.0f, 0.0f)))
                {
                    Vector3 vector = Vector3.TransformPosition(listaDeVertices[i].VectorAVector3(), escalacion);
                    listaDeVertices[i] = (Vector)vector;
                }
                else
                {
                    Vector3 verticeEnOrigen = listaDeVertices[i].VectorAVector3() - centro.VectorAVector3();
                    Vector3 vector = Vector3.TransformPosition(verticeEnOrigen, escalacion);
                    listaDeVertices[i] = (Vector)vector + centro;
                }
                // Vector3 vector = Vector3.TransformPosition(listaDeVertices[i].VectorAVector3(), escalacion);
                // listaDeVertices[i] = (Vector)vector;
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

    }
}
