using System;
using OpenTK;
using System.Collections.Generic;

namespace DiseñoRepisa
{
    public class Escenario
    {
        protected List<Objeto> listaDeObjetos;
        protected List<String> listaDeNombresDeObjetos;
        protected Vector3 centro;

        public Escenario(Vector3 centro)
        {
            this.listaDeObjetos = new List<Objeto>();
            this.listaDeNombresDeObjetos = new List<string>();
            this.centro = centro;
        }

        public void addObjeto(String nombre, Objeto nuevoObjeto)
        {
            this.listaDeNombresDeObjetos.Add(nombre);
            this.listaDeObjetos.Add(nuevoObjeto);
        }

        public void deleteObjeto(String objetoAEliminar)
        {
            int posicion = this.listaDeNombresDeObjetos.IndexOf(objetoAEliminar);
            this.listaDeObjetos.RemoveAt(posicion);
        }

        public void dibujar()
        {
            foreach(Objeto objetoActual in this.listaDeObjetos)
            {
                objetoActual.dibujarParte();
            }
        }
    }
}
