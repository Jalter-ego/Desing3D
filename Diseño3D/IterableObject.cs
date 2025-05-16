using System;
using OpenTK;
using OpenTK.Graphics;

namespace Diseño3D
{
    public interface IterableObject
    {
        void Rotar(float angulo, Vector3 eje);
        void Rotar(float angulo, Vector3 eje, Vector punto);
        void Trasladar(Vector3 delta);
        void Escalar(float escalar,Vector3 escala);
        void SetCentro(Vector v);
        void SetColor(Color4 color);
        Vector CalcularCentroMasa();
        Vector GetCentro();
    }
}
