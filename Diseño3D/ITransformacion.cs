using System;
using OpenTK;

// Archivo: ITransformacion.cs (o donde lo tengas)
// using OpenTK; // quite OpenTK.Vector3 y puse Diseño3D.Vector3D, cambia si es necesario
// using Diseño3D; // Para tu clase Vector si es diferente

namespace Diseño3D
{
    public interface ITransformacion
    {
        /// <summary>
        /// Aplica la transformación al objetivo durante un delta de tiempo.
        /// </summary>
        /// <param name="objetivo">El IterableObject a transformar.</param>
        /// <param name="tiempoDelta">El tiempo transcurrido desde el último frame.</param>
        void Aplicar(IterableObject objetivo, float tiempoDelta); // CAMBIO AQUÍ

        bool IsCompleta { get; }
        void Reset();
        float DuracionTotal { get; }

        // NUEVO: Para transformaciones que siempre actúan sobre un objetivo específico
        // independientemente del que pase la Acción.
        IterableObject ObjetivoFijo { get; }
    }

    public class Traslacion : ITransformacion
    {
        private OpenTK.Vector3 direccionPorSegundo; // Usamos OpenTK.Vector3 consistentemente aquí
        private float duracion;
        private float tiempoTranscurrido;
        // private bool usaDireccionLocal; // Podrías necesitar esto

        public bool IsCompleta => tiempoTranscurrido >= duracion;
        public float DuracionTotal => duracion;
        public IterableObject ObjetivoFijo { get; private set; } // Puede ser null

        public Traslacion(OpenTK.Vector3 direccionVelocidad, float duracionTotal, IterableObject objetivoFijo = null)
        {
            this.direccionPorSegundo = direccionVelocidad;
            this.duracion = duracionTotal;
            this.tiempoTranscurrido = 0f;
            this.ObjetivoFijo = objetivoFijo;
        }

        public void Aplicar(IterableObject objetivoPorDefecto, float tiempoDelta)
        {
            IterableObject objetivoReal = ObjetivoFijo ?? objetivoPorDefecto;
            if (objetivoReal == null || IsCompleta) return;

            float tiempoAplicar = Math.Min(tiempoDelta, duracion - tiempoTranscurrido);
            OpenTK.Vector3 movimientoDelFrame = direccionPorSegundo * tiempoAplicar;

            // Asume que tu IterableObject.Trasladar puede tomar OpenTK.Vector3
            // o tienes una conversión implícita o un método sobrecargado.
            objetivoReal.Trasladar(movimientoDelFrame);

            tiempoTranscurrido += tiempoAplicar;
        }

        public void Reset()
        {
            tiempoTranscurrido = 0f;
        }
    }

    public class Rotacion : ITransformacion
    {
        private OpenTK.Vector3 eje; // OpenTK.Vector3
        private float anguloPorSegundo;
        private float duracion;
        private float tiempoTranscurrido;
        private bool usarCentroMasaPropio;
        public IterableObject ObjetivoFijo { get; private set; } // Puede ser null


        public bool IsCompleta => tiempoTranscurrido >= duracion;
        public float DuracionTotal => duracion;

        public Rotacion(OpenTK.Vector3 ejeDeRotacion, float gradosTotales, float duracionTotal, bool usarCentroDeMasaDelElemento = true, IterableObject objetivoFijo = null)
        {
            this.eje = ejeDeRotacion;
            this.anguloPorSegundo = (duracionTotal > 0) ? gradosTotales / duracionTotal : gradosTotales;
            this.duracion = duracionTotal;
            this.tiempoTranscurrido = 0f;
            this.usarCentroMasaPropio = usarCentroDeMasaDelElemento;
            this.ObjetivoFijo = objetivoFijo;
        }

        public void Aplicar(IterableObject objetivoPorDefecto, float tiempoDelta)
        {
            IterableObject objetivoReal = ObjetivoFijo ?? objetivoPorDefecto;
            if (objetivoReal == null || IsCompleta) return;

            float tiempoAplicar = Math.Min(tiempoDelta, duracion - tiempoTranscurrido);
            float anguloDelFrame = anguloPorSegundo * tiempoAplicar;

            if (usarCentroMasaPropio)
            {
                // Asume que CalcularCentroMasa devuelve tu tipo Vector y Rotar lo acepta
                Vector centroMasa = objetivoReal.CalcularCentroMasa();
                objetivoReal.Rotar(anguloDelFrame, eje, centroMasa); // 'eje' es OpenTK.Vector3
            }
            else
            {
                objetivoReal.Rotar(anguloDelFrame, eje);
            }
            tiempoTranscurrido += tiempoAplicar;
        }

        public void Reset()
        {
            tiempoTranscurrido = 0f;
        }
    }
}
