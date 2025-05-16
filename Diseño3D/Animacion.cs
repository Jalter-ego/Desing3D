using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;

namespace Diseño3D
{
    class Animacion
    {
        public List<Escena> listaEscenas;
        public int siguiente;
        public Escena escena;

        private float tiempoTrans, ultimoTiempo;
        private float FPS;

        public Animacion()
        {
            listaEscenas = new List<Escena>();
            siguiente = -1;
            tiempoTrans = 0;
            ultimoTiempo = 0;
            FPS = 0.0f;
            escena = new Escena();
        }

        private float CalcularFPS()
        {
            float fps = 1;
            if (tiempoTrans - ultimoTiempo > 0.0)
            {
                fps = 1.0f / (tiempoTrans - ultimoTiempo);
            }
            return fps;
        }

        public void AddEscena(Escena escena)
        {
            listaEscenas.Add(escena);
        }

        public Escena SiguienteEscena()
        {
            siguiente++;
            if (siguiente < listaEscenas.Count())
            {
                return listaEscenas.ElementAt(siguiente);
            }
            return null;
        }

        private void ReiniciarLista()
        {
            siguiente = 0;
        }

        public void ejecutar(FrameEventArgs e)
        {
            float t_transcurrido = (float)e.Time;
            tiempoTrans += t_transcurrido;
            FPS = CalcularFPS();
            ultimoTiempo = tiempoTrans;

            if (escena == null)
            {
                escena = SiguienteEscena();
            }
            else
            {
                if (escena.FinEjecucion(tiempoTrans))
                    escena = SiguienteEscena();
            }

            if (!(escena == null) && !escena.FinEjecucion(tiempoTrans))
                escena.Ejecutar(tiempoTrans, FPS);

        }

    }
}
