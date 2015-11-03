using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Servicios.Models
{
    public class Solicitudes
    {
        public Solicitudes() { EstadoActual = new Estado { Id = new Guid(), EstadoActual = "Pendiente" }; }
        [Display(Name ="Servicio")]
        public Servicio Servicios
        { get; set; }
        [Display(Name = "Institución A La Que Pertenece:")]
        [Required(ErrorMessage = "La institución es un campo obligatorio")]
        public string Institucion
        { get; set; }

        [Display(Name = "Dirección:")]
        [Required(ErrorMessage = "La dirección es un campo obligatorio")]
        public string Direccion
        { get; set; }

        [Display(Name = "Telefóno:")]
        [Required(ErrorMessage = "El número de telefóno es un campo obligatorio")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Debe de ingresar un número de telefóno válido")]
        public string Telefono
        { get; set; }

        [Display(Name = "Correo Electrónico:")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
        [Required(ErrorMessage = "El correo electrónico es un campo obligatorio")]
        public string CorreoElectronico
        { get; set; }

        [Display(Name = "Cantidad De Participantes:")]
        [Required(ErrorMessage = "La cantidad de persona es un campo obligatorio")]
        [Range(1, Double.MaxValue, ErrorMessage = "La cantidad de personas debe de ser mayor que 0")]
        public int CantParticipante
        { get; set; }
        [Display(Name = "Fecha")]
        [DataType(DataType.DateTime)]
        public DateTime Fecha { get; set; }
        [Display(Name = "Adjuntar Archivos")]
        [Required(ErrorMessage = "Debe de adjuntar la carta  solicitud de la institución u organización firmada y sellada")]
        public virtual List<Archivo>Archivos
        { get; set; }

        [Required(ErrorMessage = "Debe de adjuntar la carta  solicitud de la institución u organización firmada y sellada")]
        public string NombreArchivo { get; set; }
        public virtual Solicitante Solicitantes { get; set; }
        [Display(Name ="Estado")]
        public Estado EstadoActual { get; set;}
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }
        [Display(Name = "Nota")]
        public string Nota { get; set; }
        [ScaffoldColumn(false)]
        [Display(Name = "Número De Solicitud")]
        public string NoSolicitud { get; set; }

    }
}