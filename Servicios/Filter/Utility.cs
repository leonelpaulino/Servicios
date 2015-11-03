using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios.Filter
{
    public static class Utility
    {
        public static bool isValid(string token)
        {

            Models.GateWay gateway = new Models.GateWay();
            Models.MensajeRetorno<bool> returVal = gateway.PeticionGet<bool>(token, "Tokens/get");
            if (returVal.Data)
                return true;
            else
                return false;
        }
    }
}