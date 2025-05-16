using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using OpenTK;
using OpenTK.Graphics;

namespace Diseño3D
{
    public class Escenario: ISerializable,IterableObject
    {
        public Vector centro;
        public Color4 color;
        public Dictionary<String,Objeto> listaDeObjetos;
        public Escenario(Dictionary<String,Objeto> list,Vector centro)
        {
            this.listaDeObjetos = list;
            this.centro = centro;
            this.color = new Color4(1.0f, 0.3f, 0.3f, 1f);
        }
        public void AddObjeto(String nombre, Objeto nuevoObjeto)
        {
            this.listaDeObjetos.Add(nombre, nuevoObjeto);
        }


        public Objeto GetObjeto(String nombre)
        {
            return this.listaDeObjetos[nombre];
        }

        public void SetObjeto(String name,Objeto obj)
        {
            this.listaDeObjetos[name] = obj;
        }

        public Vector GetCentro()
        {
            return this.centro;
        }

        public void SetCentro(Vector nuevoCentro)
        {
            this.centro = nuevoCentro;
            foreach (Objeto objeto in listaDeObjetos.Values)
            {
                objeto.SetCentro(nuevoCentro);
            }
        }

        public void SetColor(Color4 color)
        {
            this.color = color;
            foreach (Objeto objeto in listaDeObjetos.Values)
            {
                objeto.SetColor(color);
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

        public void Draw()
        {
            foreach (Objeto objeto in this.listaDeObjetos.Values)
            {
                objeto.Draw();
            }
        }

        public void Rotar(float angulo, Vector3 eje)
        {
            foreach (Objeto obj in this.listaDeObjetos.Values)
            {
                obj.Rotar(angulo, eje);
            }
        }

        public void Rotar(float angulo, Vector3 eje, Vector puntoFijo)
        {
            foreach (Objeto obj in listaDeObjetos.Values)
            {
                obj.Rotar(angulo, eje, puntoFijo);
            }
        }

        public void Escalar(float escalar, Vector3 factor)
        {
            foreach (Objeto obj in this.listaDeObjetos.Values)
            {
                obj.Escalar(escalar, factor);
            }
        }

        public void Trasladar(Vector3 otroCentro)
        {
            foreach (Objeto obj in this.listaDeObjetos.Values)
            {
                obj.Trasladar(otroCentro);
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable)listaDeObjetos).GetObjectData(info, context);
        }

    }
}
