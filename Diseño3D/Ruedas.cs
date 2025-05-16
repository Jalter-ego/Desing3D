using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diseño3D
{
    public class Ruedas
    {
        public Dictionary<String, ObjLoader> listaDeLlantas;

        public Ruedas()
        {
            listaDeLlantas = new Dictionary<String, ObjLoader>();
        }

        public void AddRueda(String nombre, Vector centro,ObjLoader obj)
        {
            obj.Rotar(90, new Vector(1, 0, 0).VectorAVector3());
            obj.Rotar(90, new Vector(0, 0, 1).VectorAVector3());
            obj.SetCentro(centro.VectorAVector3());
            listaDeLlantas.Add(nombre,obj);
        }
    }
}
