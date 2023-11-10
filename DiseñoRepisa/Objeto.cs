using OpenTK;
using System;
using OpenTK.Graphics;
using System.Collections.Generic;
using System.Runtime.Serialization; // Agrega este using para habilitar la serialización.

namespace DiseñoRepisa
{
    public class Objeto : ISerializable
    {
        public Dictionary<String,Partes> listaDePartes;
        public Color4 color;
        public Punto centro;
        public Objeto()
        {
            this.listaDePartes = new Dictionary<String,Partes>();
            this.color = new Color4(0,0,0,0);
        }

        public void addParte(String nombre,Partes nuevaParte)
        {
            listaDePartes.Add(nombre,nuevaParte);
        }

        public void deleteParte(String nombre)
        {
            this.listaDePartes.Remove(nombre);
        }
        
        public Partes getParte(String nombre)
        {
            return this.listaDePartes[nombre];
        }

        public void escalar(float factorDeEscala)
        {
            foreach (Partes parteActual in listaDePartes.Values)
            {
                parteActual.escalar(factorDeEscala);
            }
        }

        public void Rotar(float angulo, String eje)
        {
            foreach (var parte in listaDePartes.Values)
            {
                parte.Rotar(angulo, eje);
            }
        }

        public void Trasladar(Vector3 traslacion)
        {
            foreach (var parte in listaDePartes.Values)
            {
                parte.Trasladar(traslacion);
            }
        }

        public void dibujarParte(Vector3 centro)
        {
            foreach (Partes partes in listaDePartes.Values)
            {
                partes.dibujarPoligono(centro);
            }
        }

        public void setColor(String parte,String poligono,Color4 color)
        {
            this.color = color;
            listaDePartes[parte].setColor(poligono,this.color);
        }

        public void setCentro(Punto centro)
        {
           // this.centro = centro;
            foreach (Partes parteActual in listaDePartes.Values)
            {
                parteActual.setCentro(centro);
            }
        }

        public void desplazar(Vector3 vector)
        {
            foreach (Partes parte in listaDePartes.Values)
            {
                parte.desplazar(vector);
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable)listaDePartes).GetObjectData(info, context);
        }
    }
}
