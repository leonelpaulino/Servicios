using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Models
{
    public class Solicitante
    {
        public Guid Id { get; set; }
        public Solicitante() {}
        [Display(Name ="Nombre Completo")]
        [Required(ErrorMessage = "Debe de llenar el campo del nombre")]
        public string NombreCompleto { get; set; }
        [Display(Name ="Cédula")]
        [Required(ErrorMessage ="Debe de llenar el campo de la cédula.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{7})[-. ]?([0-9]{1})$", ErrorMessage = "Debe de ingresar un número de cédula válido")]
        public string Cedula { get; set; }
        [Display(Name = "Correo Electrónico:")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
        [Required(ErrorMessage = "El correo electrónico es un campo obligatorio")]
        public string CorreoElectronico
        { get; set; }
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Necesita llenar la contraseña")]
        [DataType(DataType.Password)]
        [StringLength(7)]
        public string Clave { get; set; }
        [Display(Name = "Confirmar Contraseña")]
        [Required(ErrorMessage = "Necesita confirmar la contraseña")]
        [DataType(DataType.Password)]
        [Compare("Clave",ErrorMessage ="La contraseña debe ser la misma")]
        public string ConfirmarClave { get; set; }

    }
}
