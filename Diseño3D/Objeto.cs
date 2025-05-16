using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using System.Runtime.Serialization;

namespace Diseño3D
{
    public class Objeto: ISerializable, IterableObject
    {
        public Dictionary<String,Parte> listaDePartes;
        public Vector centro;
        public Color4 color;


        public Objeto(Dictionary<String,Parte> list, Vector centro)
        {
            this.listaDePartes = list;
            this.centro = centro;
            this.color = new Color4(1.0f, 0.3f, 0.3f, 1f);
        }

        public void AddParte(String nombre, Parte nuevaParte)
        {
            listaDePartes.Add(nombre, nuevaParte);
        }

        public Vector GetCentro()
        {
            return this.centro;
        }



        public void SetColor(Color4 color)
        {
            this.color = color;
            foreach (Parte parteActual in listaDePartes.Values)
            {
                parteActual.SetColor(color);
            }
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

        public void Draw3()
        {
            foreach (Parte parte in this.listaDePartes.Values)
            {
                parte.Draw3();
            }
        }
        public void SetCentro(Vector nuevoCentro)
        {
            this.centro = nuevoCentro;
            foreach (Parte parteActual in listaDePartes.Values)
            {
                parteActual.SetCentro(nuevoCentro);
            }
        }

        public Vector CalcularCentroMasa()
        {
            Vector sumCentro = new Vector(0.0f, 0.0f, 0.0f);
            foreach (Parte parte in listaDePartes.Values)
            {
                sumCentro += parte.CalcularCentroMasa();
            }
            if (listaDePartes.Count == 0) return this.centro;
            sumCentro /= listaDePartes.Count;
            return sumCentro;
        }

        public void Rotar(float angulo, Vector3 eje)
        {
            foreach (Parte parte in this.listaDePartes.Values)
            {
                parte.Rotar(angulo, eje);
            }
        }

        public void Rotar(float angulo, Vector3 eje, Vector puntoFijo)
        {
            foreach (Parte pol in listaDePartes.Values)
            {
                pol.Rotar(angulo, eje, puntoFijo);
            }
        }

        public void Trasladar(Vector3 delta)
        {
            foreach (Parte parte in this.listaDePartes.Values)
            {
                parte.Trasladar(delta);
            }
            this.centro += new Vector(delta.X, delta.Y, delta.Z);
        }

        public void Escalar(float escalar, Vector3 factor)
        {
            foreach (Parte parte in this.listaDePartes.Values)
            {
                parte.Escalar(escalar, factor);
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable)listaDePartes).GetObjectData(info, context);
        }

    }


}


