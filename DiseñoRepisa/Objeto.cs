using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace DiseñoRepisa
{
    public class Objeto
    {
        public List<Partes> listaDePartes;
        public Objeto()
        {
            this.listaDePartes = new List<Partes>();
        }

        public void add(Vector3 centro,Vector3 dimensiones, Color4 color, int x, int y, int z)
        {
            Poligono nuevoPoligono = new Poligono(centro,dimensiones, color, x, y, z);
            Partes nuevaParte = new Partes();
            nuevaParte.add(nuevoPoligono);
            listaDePartes.Add(nuevaParte);
        }
    }
}
