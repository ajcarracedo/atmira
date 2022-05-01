using System;

namespace Backend.DTO.APIResponse
{
    public class APIResponseDTO
    {
        /// <summary>
        /// Nombre del objeto
        /// </summary>
        public string nombre { get; set; }

        /// <summary>
        /// En kilómetros
        /// </summary>
        public float diametro { get; set; }

        /// <summary>
        /// En kilómetros por hora
        /// </summary>
        public float velocidad { get; set; }

        /// <summary>
        /// Fecha de máxima aproximación
        /// </summary>
        public DateTime fecha { get; set; }

        /// <summary>
        /// Nombre del planeta que orbita
        /// </summary>
        public string planeta { get; set; }

    }
}