using System;
using System.Collections.Generic;
using OpenTK;

namespace Diseño3D
{
    public class Escenario
    {
        private Vector centro;
        private Dictionary<String,Objeto> listaDeObjetos;

        public Escenario(Dictionary<String,Objeto> list,Vector centro)
        {
            this.listaDeObjetos = list;
            this.centro = centro;
        }
        public void AddObjeto(String nombre, Objeto nuevoObjeto)
        {
            this.listaDeObjetos.Add(nombre, nuevoObjeto);
        }


        public Objeto GetObjeto(String nombre)
        {
            return this.listaDeObjetos[nombre];
        }

        public Vector GetCentro()
        {
            return this.centro;
        }

        public void SetCentro(Vector centro)
        {
            this.centro = centro;
            foreach (Objeto objeto in listaDeObjetos.Values)
            {
                objeto.SetCentro(centro);
            }
        }

        public void Draw()
        {
            foreach (Objeto objeto in this.listaDeObjetos.Values)
            {
                objeto.Draw();
            }
        }

        public Vector CalcularCentroMasa()
        {
            Vector sumCentro = new Vector(0.0f, 0.0f, 0.0f);
            foreach (Objeto objeto in listaDeObjetos.Values)
            {
                sumCentro += objeto.CalcularCentroMasa();
            }
            sumCentro /= listaDeObjetos.Count;
            return sumCentro;
        }

        public void TrasladarObjetosASuOrigen()
        {
            foreach (Objeto obj in this.listaDeObjetos.Values)
            {
                Vector3 centroOrigen = obj.centro.VectorAVector3();
                obj.Trasladar(centroOrigen);
            }
        }

    }
}
