using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace DiseñoRepisa
{
    public class Partes
    {
        public List<Poligono> listaDePartes;
        public Partes()
        {
            listaDePartes = new List<Poligono>();
        }

        public void add(Poligono poligono)
        {
            this.listaDePartes.Add(poligono);
        }

        public void delete(Poligono poligono)
        {
            int i = this.listaDePartes.IndexOf(poligono);
            this.listaDePartes.RemoveAt(i);
        }

        public void dibujarPoligono()
        {
            foreach (Poligono poligono in listaDePartes)
            {
                poligono.Dibujar();
            }
        }
    }
}
