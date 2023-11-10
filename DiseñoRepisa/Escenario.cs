using System;
using OpenTK;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DiseñoRepisa
{
    public class Escenario: ISerializable
    {
        public Dictionary<String, Objeto> listaDeObjetos;
        protected Vector3 centro;

        public Escenario(Vector3 centro)
        {
            this.listaDeObjetos = new Dictionary<String, Objeto>();
            this.centro = centro;
        }

        public void addObjeto(String nombre, Objeto nuevoObjeto)
        {
            this.listaDeObjetos.Add(nombre, nuevoObjeto);
        }

        public void deleteObjeto(String objetoAEliminar)
        {
            this.listaDeObjetos.Remove(objetoAEliminar);
        }

        public Objeto getObjeto(String nombre)
        {
            return this.listaDeObjetos[nombre];
        }

        public void setObjeto(String nombre, Objeto objeto)
        {
            this.listaDeObjetos[nombre] = objeto;
        }
        
        public void dibujar(Vector3 centro)
        {
            foreach(Objeto objetoActual in this.listaDeObjetos.Values)
            {
                objetoActual.dibujarParte(centro);
            }
        }

        public void escalar(float factorDeEscala)
        {
            foreach (Objeto objetoActual in listaDeObjetos.Values)
            {
                objetoActual.escalar(factorDeEscala);
            }
        }

        public void Rotar(float angulo, String eje)
        {
            foreach (var objeto in listaDeObjetos.Values)
            {
                objeto.Rotar(angulo, eje);
            }
        }

        public void Trasladar(Vector3 traslacion)
        {
            foreach (var objeto in listaDeObjetos.Values)
            {
                objeto.Trasladar(traslacion);
            }
        }
        
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable)listaDeObjetos).GetObjectData(info, context);
        }
    }
}
