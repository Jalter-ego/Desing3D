using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Diseño3D
{
    public class Objeto
    {
        public Dictionary<String,Parte> listaDePartes;
        public Vector centro;
        public Color4 color;

        public Objeto(Dictionary<String,Parte> list, Vector centro)
        {
            this.listaDePartes = list;
        }

        public void AddParte(String nombre, Parte nuevaParte)
        {
            listaDePartes.Add(nombre, nuevaParte);
        }

        public Vector GetCentro()
        {
            return this.centro;
        }

        public void SetCentro(Vector centro)
        {
            this.centro = centro;
            foreach (Parte parteActual in listaDePartes.Values)
            {
                parteActual.SetCentro(centro);
            }
        }

        public void SetColor(String parte, String poligono, Color4 color)
        {
            this.color = color;
            listaDePartes[parte].SetColor(poligono, this.color);
        }

        public Parte GetParte(String nombre)
        {
            return this.listaDePartes[nombre];
        }

        public void Draw()
        {
            foreach (Parte parte in this.listaDePartes.Values)
            {
                parte.Draw();
            }
        }

        public Vector CalcularCentroMasa()
        {
            Vector sumCentro = new Vector(0.0f, 0.0f, 0.0f);
            foreach (Parte parte in listaDePartes.Values)
            {
                sumCentro += parte.CalcularCentroMasa();
            }
            sumCentro /= listaDePartes.Count;
            return sumCentro;
        }


    }


}


