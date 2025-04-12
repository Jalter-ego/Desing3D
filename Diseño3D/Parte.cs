using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using OpenTK;
using OpenTK.Graphics;

namespace Diseño3D
{
    public class Parte: ISerializable
    {
        public Dictionary<String,Poligono> listaDePoligonos;
        public Vector centro;
        public Color4 color;

        public Parte()
        {
            this.listaDePoligonos = new Dictionary<String,Poligono>();
            this.color = new Color4(0, 0, 0, 0);
            this.centro = new Vector(0, 0, 0);
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

        public void SetCentro(Vector nuevoCentro)
        {
            this.centro = nuevoCentro;
            foreach (Poligono poligono in listaDePoligonos.Values)
            {
                poligono.SetCentro(nuevoCentro);
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

        public void Rotar(float angulo, Vector3 eje)
        {
            foreach (Poligono poligono in this.listaDePoligonos.Values)
            {
                poligono.Rotar(angulo, eje);
            }
        }

        public void Trasladar(Vector3 otroCentro)
        {
            foreach (Poligono poligono in this.listaDePoligonos.Values)
            {
                poligono.Trasladar(otroCentro);
            }
            centro = CalcularCentroMasa();
        }

        public void Escalar(float escalar, Vector3 factor)
        {
            foreach (Poligono poligono in this.listaDePoligonos.Values)
            {
                poligono.Escalar(escalar, factor);
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable)listaDePoligonos).GetObjectData(info, context);
        }

    }
}
