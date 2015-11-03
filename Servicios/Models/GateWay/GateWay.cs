using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace Servicios.Models
{
    public partial class GateWay
    {
        private string apiBaseAddress;
        public string Token { get; set; }
        public GateWay() {
            apiBaseAddress = "http://dynamicscrm01:8080/api/";
            Token = "";
            
        }
        /// <summary>
        /// Hace una peticion Post al servicio ProxyCRM.
        /// </summary>
        /// <typeparam name="T">Tipo de dato con la que se va hacer la peticion</typeparam>
        /// <typeparam name="U">Tipo de dato de retorno de la funcion</typeparam>
        /// <param name="param">parametro que se va enviar</param>
        /// <param name="function">funcion que se va utilizar del web service</param>
        /// <returns> Retorna un objeto mensaje de retorno.</returns>
        public MensajeRetorno<U> PeticionPost<T,U>(T param,string function)
        {
            HttpClient client = new HttpClient();
            if (Token != "")
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic",Token);
            HttpResponseMessage response  = client.PostAsJsonAsync(apiBaseAddress +function, param).Result;
            return Retorno<U>(response);
        }
        /// <summary>
        /// Hace una peticion Get al servicio ProxyCRM.
        /// </summary>
        /// <typeparam name="T">Tipo de dato con la que se va hacer la peticion</typeparam>
        /// <typeparam name="U">Tipo de dato de retorno de la funcion</typeparam>
        /// <param name="param">parametro que se va enviar</param>
        /// <param name="function">funcion que se va utilizar del web service</param>
        /// <returns> Retorna un objeto mensaje de retorno.</returns>
        public MensajeRetorno<T> PeticionGet<T>(string param, string function)
        {
            HttpClient client = new HttpClient();
            string parameters = "";
            if (Token != "")
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", Token);
            if (param != "")
                parameters = "/?id=" + param;
            HttpResponseMessage response = client.GetAsync(apiBaseAddress + function +parameters).Result;
            return Retorno<T>(response);
        }
        /// <summary>
        /// Hace una peticion Put al servicio ProxyCRM.
        /// </summary>
        /// <typeparam name="T">Tipo de dato con la que se va hacer la peticion</typeparam>
        /// <typeparam name="U">Tipo de dato de retorno de la funcion</typeparam>
        /// <param name="param">parametro que se  va enviar</param>
        /// <param name="function">funcion que se va utilizar del web service</param>
        /// <returns> Retorna un objeto mensaje de retorno.</returns>
        public MensajeRetorno<U> PeticionPut<T,U>(T param, string function)
        {
            HttpClient client = new HttpClient();
            if (Token != "")
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", Token);
            HttpResponseMessage response = client.PutAsJsonAsync(apiBaseAddress + function, param ).Result;
            return  Retorno<U>(response);
        }
        /// <summary>
        /// Construye el mensaje de retorno de las funciones.
        /// </summary>
        /// <typeparam name="T">Tipo de dato que se va retornar</typeparam>
        /// <returns> Retorna un objeto mensaje de retorno.</returns>
        private MensajeRetorno<T> Retorno<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                string responseString = response.Content.ReadAsStringAsync().Result;
                MensajeRetorno<T> returVal = JsonConvert.DeserializeObject<MensajeRetorno<T>>(responseString);
                return returVal;
            }
            return new MensajeRetorno<T> { Message = "Error al conectarse al servicio. Error:" + response.StatusCode + " " + response.ReasonPhrase };
        }

    }
}