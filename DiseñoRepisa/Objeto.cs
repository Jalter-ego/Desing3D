using OpenTK;
using OpenTK.Graphics;
using System.Collections.Generic;

namespace DiseñoRepisa
{
    public class Objeto
    {
        public List<Partes> listaDePartes;
        public Objeto()
        {
            this.listaDePartes = new List<Partes>();
        }

        public void addParte(Vector3 centro,Vector3 dimensiones, Color4 color, float x, float y, float z)
        {
            Poligono nuevoPoligono = new Poligono(centro,dimensiones, color, x, y, z);
            Partes nuevaParte = new Partes();
            nuevaParte.add(nuevoPoligono);
            listaDePartes.Add(nuevaParte);
        }

        public void deleteParte(Partes parte)
        {
            this.listaDePartes.Remove(parte);
        }

        public void dibujarParte()
        {
            foreach (Partes partes in listaDePartes)
            {
                partes.dibujarPoligono();
            }
        }
    }
}
