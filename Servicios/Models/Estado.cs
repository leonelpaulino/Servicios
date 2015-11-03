using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios.Models
{
    public class Estado
    {
        public Guid Id { get; set; }
        public string EstadoActual { get; set; }
        public int Posicion { get; set; }
    }
}