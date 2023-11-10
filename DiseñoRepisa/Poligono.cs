using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DiseñoRepisa
{
    public class Poligono
    {
        public List<Punto> Vertices;
        public Color4 color;
        public Punto centro;

        public Poligono(Color4 color)
        {
            this.Vertices = new List<Punto>();
            this.color = color;
            this.centro = new Punto(0,0,0);
        }

        public void addVertice(float x,float y, float z)
        {
            Vertices.Add(new Punto(x,y,z));
        }

        public void setColor(Color4 color)
        {
            this.color = color;
        }

        public void setCentro(Punto centro)
        {
            this.centro = centro;
        }

        public void Dibujar(Vector3 centro)
        {
            GL.PushMatrix();
            GL.Translate(centro + new Vector3(0.1f, -0.5f, -1.7f));

            GL.Begin(PrimitiveType.Quads);
            GL.Color4(color: color);

            foreach (var vertice in Vertices)
            {
                GL.Vertex3(new Vector3(vertice.x,vertice.y,vertice.z));
            }

            GL.End();
            GL.PopMatrix();
        }

        public void Rotar(float angle,String eje)
        {
            if (eje == "x" || eje == "X")
            {
                for (int i = 0; i < Vertices.Count; i++)
                {
                    Vertices[i] = RotateVertexX(Vertices[i], angle);
                }
            }
            if (eje == "y" || eje == "Y")
            {
                for (int i = 0; i < Vertices.Count; i++)
                {
                    Vertices[i] = RotateVertexY(Vertices[i], angle);
                }
            }
            if (eje == "z" || eje == "Z")
            {
                for (int i = 0; i < Vertices.Count; i++)
                {
                    Vertices[i] = RotateVertexZ(Vertices[i], angle);
                }
            }
        }

        public void Escalar(float scaleFactor)
        {
            for (int i = 0; i < Vertices.Count; i++)
            {
                //Vertices[i] *= 1.0f / scaleFactor;
                Vertices[i].x *= scaleFactor;
                Vertices[i].y *= scaleFactor;
                Vertices[i].z *= scaleFactor;
            }
        }

        public void Trasladar(Vector3 translation)
        {
            for (int i = 0; i < Vertices.Count; i++)
            {
                Vertices[i].x += translation.X;
                Vertices[i].y += translation.Y;
                Vertices[i].z += translation.Z;
            }
        }
        private Punto RotateVertexZ(Punto vertex, float angle)
        {
            float cosA = (float)Math.Cos(angle);
            float sinA = (float)Math.Sin(angle);
            float x = vertex.x * cosA - vertex.y * sinA;
            float y = vertex.x * sinA + vertex.y * cosA;
            return new Punto(x, y, vertex.z);
        }
        private Punto RotateVertexY(Punto vertex, float angle)
        {
            float cosA = (float)Math.Cos(angle);
            float sinA = (float)Math.Sin(angle);
            float x = vertex.x * cosA - vertex.z * sinA;
            float z = vertex.x * sinA + vertex.z * cosA;
            return new Punto(x, vertex.y, z);
        }

        private Punto RotateVertexX(Punto vertex, float angle)
        {
            float cosA = (float)Math.Cos(angle);
            float sinA = (float)Math.Sin(angle);
            float y = vertex.y * cosA - vertex.z * sinA;
            float z = vertex.y * sinA + vertex.z * cosA;
            return new Punto(vertex.x, y, z);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
