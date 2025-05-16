using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diseño3D
{
    class Escena
    {
        public List<Accion> listaAcciones;
        public int siguiente;

        public float inicioTiempo;
        public float duracionTiempo;

        public Escena()
        {
            listaAcciones = new List<Accion>();
            siguiente = -1;
        }

        public void AddAccion(Accion accion, float iniTiempo, float duracionTiempo)
        {
            listaAcciones.Add(accion);
            inicioTiempo = iniTiempo;
            this.duracionTiempo = duracionTiempo;
        }

        public Accion GetEscenaActual()
        {
            if (siguiente >= 0 && siguiente < listaAcciones.Count() - 1)
            {
                return listaAcciones[siguiente];
            }
            return null;
        }

        public Boolean EsTiempoIniciar(float tiempoTrancurrido)
        {
            return tiempoTrancurrido >= inicioTiempo;
        }

        public Boolean FinEjecucion(float tiempoTrancurrido)
        {
            return tiempoTrancurrido >= (inicioTiempo + duracionTiempo);
        }

        public void Ejecutar(float tiempoTranscurrido, float FPS)
        {
            for (int i = 0; i < listaAcciones.Count(); i++)
            {
                Accion accion = listaAcciones[i];
                if (accion.HayTransformaciones())
                {
                    //ejecutar las acciones correspondientes
                    accion.Ejecutar(tiempoTranscurrido, FPS);
                }
            }
        }

    }
}
