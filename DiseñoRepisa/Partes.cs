using System;
using OpenTK;
using OpenTK.Graphics;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DiseñoRepisa
{
    public class Partes : ISerializable
    {
        public Dictionary<String, Poligono> listaDePoligonos;
        public Color4 color;
        //public Vector3 centro;

        public Partes()
        {
            listaDePoligonos = new Dictionary<string, Poligono>();
            this.color = new Color4(0,0,0,0);
        }

        public void add(String nombre, Poligono poligono)
        {
            this.listaDePoligonos.Add(nombre,poligono);
        }

        public void delete(String nombre)
        {
            this.listaDePoligonos.Remove(nombre);
        }

        public Poligono getPoligono(String nombre)
        {
            return this.listaDePoligonos[nombre];
        }

        public void setColor(String nombre,Color4 color)
        {
            this.color = color;
            listaDePoligonos[nombre].setColor(this.color);
        }

        public void setCentro(Punto centro)
        {
           // this.centro = centro;
            foreach (Poligono poligono in listaDePoligonos.Values)
            {
                poligono.setCentro(centro);
            }
        }

        public void escalar(float factorDeEscala)
        {
            foreach (Poligono poligonoActual in listaDePoligonos.Values)
            {
                poligonoActual.Escalar(factorDeEscala);
            }
        }

        public void Rotar(float angulo, String eje)
        {
            foreach (var poligono in listaDePoligonos.Values)
            {
                poligono.Rotar(angulo, eje);
            }
        }

        public void Trasladar(Vector3 traslacion)
        {
            foreach (var poligono in listaDePoligonos.Values)
            {
                poligono.Trasladar(traslacion);
            }
        }

        public void dibujarPoligono(Vector3 centro)
        {
            foreach (Poligono poligono in listaDePoligonos.Values)
            {
                poligono.Dibujar(centro);
            }
        }

        public void desplazar(Vector3 vector)
        {
            foreach (Poligono poligono in listaDePoligonos.Values)
            {
              //  poligono.desplazamiento(vector);
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable)listaDePoligonos).GetObjectData(info, context);
        }
    }
}
