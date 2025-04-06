using System;
using OpenTK;

namespace Diseño3D
{
    public class Vector
    {
        public float x, y, z;
        public Vector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector operator +(Vector p1, Vector p2)
        {
            Vector nuevoVector = new Vector(p1.x + p2.x, p1.y + p2.y, p1.z + p2.z);
            return nuevoVector;
        }

        public static Vector operator *(Vector p1, float escalar)
        {
            Vector nuevoVector = new Vector(p1.x * escalar, p1.y * escalar, p1.z * escalar);
            return nuevoVector;
        }

        public static Vector operator /(Vector p1, float escalar)
        {
            Vector nuevoVector = new Vector(p1.x / escalar, p1.y / escalar, p1.z / escalar);
            return nuevoVector;
        }

        public Vector3 VectorAVector3()
        {
            Vector3 vector = new Vector3(this.x, this.y, this.z);
            return vector;
        }

        public static explicit operator Vector(Vector3 vector)
        {
            return new Vector(vector.X, vector.Y, vector.Z);
        }
    }
}
