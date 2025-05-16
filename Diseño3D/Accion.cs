using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;

namespace Diseño3D
{
    public enum TipoTransformacion
    {
        Rotar,
        Trasladar,
        Escalar
    };

    public class Transformacion
    {
        //public IDibujable Objeto { get; set; }
        public IterableObject objeto { get; set; }
        public TipoTransformacion Tipo { get; set; }
        public Vector3 Eje { get; set; }
        public float ValorTransformacion { get; set; }
    }

    public class Accion
    {
        public List<Transformacion> Transformaciones;

        public float inicioTiempo { get; set; }
        public float duracionTiempo { get; set; }
        public float tiempoTrans { get; set; }

        public Accion()
        {
            Transformaciones = new List<Transformacion>();
        }

        public void AddAccion(IterableObject obj, TipoTransformacion tipo, Vector3 eje, float valorTransf,
            float iniTiempo, float duracionTiempo)
        {
            Transformaciones.Add(new Transformacion
            {
                Eje = eje,
                Tipo = tipo,
                ValorTransformacion = valorTransf,
                objeto = obj
            });
            inicioTiempo = iniTiempo;
            this.duracionTiempo = duracionTiempo;
        }

        public void DeleteAccion(int pos)
        {
            if (pos >= 0 && pos < Transformaciones.Count())
            {
                Transformaciones.RemoveAt(pos);
            }
        }

        public Transformacion GetTransformacionPos(int pos)
        {
            if (pos >= 0 && pos < Transformaciones.Count())
            {
                return Transformaciones[pos];
            }
            return null;
        }

        public int GetTamanioLista()
        {
            return Transformaciones.Count();
        }

        public Boolean HayTransformaciones()
        {
            return Transformaciones.Count() > 0;
        }

        public void Ejecutar(float tiempoTrans, float FPS)
        {
            this.tiempoTrans = tiempoTrans;
            if (tiempoTrans >= inicioTiempo)
            {
                if (tiempoTrans <= (inicioTiempo + duracionTiempo))
                {
                    foreach (Transformacion t1 in Transformaciones)
                    {
                        AplicarTransformacion(t1, FPS);
                    }
                }
            }
        }

        // Este método va dentro de la clase Diseño3D.Accion

        public void AplicarTransformacion(Transformacion transformacionActual, float FPS)
        {
            // Validaciones básicas
            if (transformacionActual.objeto == null)
            {
                return;
            }
            if (this.duracionTiempo <= 0)
            {
                return; // Evitar división por cero y comportamiento indefinido.
            }
            if (FPS <= 0)
            {
                return; // Evitar división por cero.
            }

            float tiempoDelFrame = 1.0f / FPS;

            // Para Rotar y Trasladar, calculamos un delta lineal del valor total.
            float cantidadLinealParaEsteFrame = (transformacionActual.ValorTransformacion / this.duracionTiempo) * tiempoDelFrame;

            switch (transformacionActual.Tipo)
            {
                case TipoTransformacion.Rotar:
                    Vector centroMasaRot = transformacionActual.objeto.CalcularCentroMasa(); // Asume que esto devuelve tu clase Vector
                    transformacionActual.objeto.Rotar(cantidadLinealParaEsteFrame, transformacionActual.Eje, centroMasaRot);
                    break;

                case TipoTransformacion.Trasladar:
                    Vector3 direccionTrasl = transformacionActual.Eje;
                    if (direccionTrasl.LengthSquared > 0.0001f) // Evitar normalizar vector cero y problemas de precisión
                    {
                        direccionTrasl.Normalize();
                    }
                    else
                    {
                        return;
                    }
                    transformacionActual.objeto.Trasladar(direccionTrasl * cantidadLinealParaEsteFrame);
                    break;
            }
        }
    }
}
