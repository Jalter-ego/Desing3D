using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Diseño3D
{
    public class Objeto
    {
        public float cx = 0;
        public float cy = 0;
        public float cz = 0;
        private List<Vector3> listaDeVertices;

        public Objeto(List<Vector3> array, Vector3 centro)
        {
            this.listaDeVertices = array;
            this.cx = centro.X;
            this.cy = centro.Y;
            this.cz = centro.Z;
        }

        public void Draw()
        {
            GL.PushMatrix();
            GL.Color3(1.0f, 0.3f, 0.3f);
            GL.Begin(PrimitiveType.Quads);

            foreach (Vector3 vector in this.listaDeVertices)
            {
                GL.Vertex3(vector.X + cx, vector.Y + cy, vector.Z +cz);
            }

            GL.End();
            GL.PopMatrix();
        }

        public void SetCentro(float x, float y, float z)
        {
            this.cx = x;
            this.cy = y;
            this.cz = z;
        }
    }   


}


