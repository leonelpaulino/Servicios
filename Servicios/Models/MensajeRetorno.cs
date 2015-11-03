using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.Models
{
    /// <summary>
    /// Estado del mensaje. ( se corresponde con los estados definidos en el enum States.
    /// </summary>
   public class MensajeRetorno<T>
    {
        /// <summary>
        /// Estado del mensaje
        /// </summary>
        public string State
        {
            get; set;
        }
        /// <summary>
        /// Mensaje.
        /// </summary>
        public string Message
        {
            get; set;
        }
        /// <summary>
        /// Informacion adicional
        /// </summary>
        public T Data
        {
            get; set;
        }
    }
}
