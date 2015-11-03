using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Servicios.Models
{
    public class Archivo
    {
        public Guid ArchivoId { get; set; }
        public string NombreArchivo { get; set; }
        public string TipoDeArchivo { get; set; }
        public byte[] Contenido { get; set; }
        public int Longitud { get; set; }
    }
}