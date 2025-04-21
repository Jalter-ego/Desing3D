using System;
using OpenTK;

namespace Diseño3D
{
    interface IterableObject
    {
        void Rotar(float angulo, Vector3 eje);
        void Trasladar(Vector3 delta);
        void Escalar(float escalar,Vector3 escala);
        void SetCentro(Vector v);
        Vector CalcularCentroMasa();
        Vector GetCentro();
    }
}
