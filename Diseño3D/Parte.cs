using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;

namespace Diseño3D
{
    public class Parte
    {
        public Dictionary<String,Poligono> listaDePoligonos;
        public Vector centro;
        public Color4 color;

        public Parte()
        {
            this.listaDePoligonos = new Dictionary<String,Poligono>();
            this.color = new Color4(0, 0, 0, 0);
        }

        public void Add(String nombre, Poligono p)
        {
            this.listaDePoligonos.Add(nombre,p);
        }

        public Poligono GetPoligono(String nombre)
        {
            return this.listaDePoligonos[nombre];
        }

        public Vector GetCentro()
        {
            return this.centro;
        }

        public void SetCentro(Vector centro)
        {
            this.centro = centro;
            foreach (Poligono poligono in listaDePoligonos.Values)
            {
                poligono.SetCentro(centro);
            }
        }
        public void SetColor(String nombre, Color4 color)
        {
            this.color = color;
            listaDePoligonos[nombre].SetColor(this.color);
        }

        public Vector CalcularCentroMasa()
        {
            Vector sumCentro = new Vector(0.0f, 0.0f, 0.0f);
            foreach (Poligono poligono in listaDePoligonos.Values)
            {
                sumCentro += poligono.CalcularCentroMasa();
            }
            sumCentro /= listaDePoligonos.Count;
            return sumCentro;
        }


        public void Draw()
        {
            foreach (Poligono poligono in this.listaDePoligonos.Values)
            {
                poligono.Draw();
            }
        }


    }
}
