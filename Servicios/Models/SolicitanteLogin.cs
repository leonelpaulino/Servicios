using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Models
{
    public class LoginSolicitante
    {
        [Display(Name = "Usuario/Cédula")]
        [Required(ErrorMessage ="Necesita llenar el usuario/Cédula.")]
        public string Usuario { get; set; }
        [Display(Name ="Contraseña")]
        [Required(ErrorMessage ="Necesita llenar la contraseña")]
        [DataType(DataType.Password)]
        public string Clave { get; set; }
    }
}
