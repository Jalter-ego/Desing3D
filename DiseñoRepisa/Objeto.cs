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

        public void addParte(Partes nuevaParte)
        {
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
