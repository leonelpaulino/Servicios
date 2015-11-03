using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios.Models
{
    public partial class GateWay
    {
        /// <summary>
        /// Verifica si un token todavia existe en el api.
        /// </summary>
        /// <param name="id">token</param>
        /// <returns>Retorna un mensaje de retorno.</returns>
        public MensajeRetorno<bool> TokenExiste(string id)
        {
            return PeticionGet<bool>(id, "Tokens/get");
        }
    }
}