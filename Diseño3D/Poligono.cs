using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Diseño3D
{
    public class Poligono
    {
        public List<Vector> listaDeVertices;
        public Color4 color;
        public Vector centro;

        public Poligono(Color4 color)
        {
            this.listaDeVertices = new List<Vector>();
            this.color = color;
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
            this.centro = this.CalcularCentroMasa();
        }

        public void Draw()
        {
            //1.0f, 0.3f, 0.3f
            GL.Color4(color);
            GL.Begin(PrimitiveType.Quads);

            foreach (Vector vector in this.listaDeVertices)
            {
                GL.Vertex3(vector.x , vector.y, vector.z);
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


    }
}
