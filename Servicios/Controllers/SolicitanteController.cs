using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Servicios.Models;
using Servicios.Filter;
namespace Servicios.Controllers
{
    public class SolicitanteController : BaseController
    {
        [LoginFilter]
        public ActionResult Login()
        {
            ViewBag.Message = "";
            ViewBag.Error = "";
            return View();
        }
        [AuthenticationFilter]
        public ActionResult Logout()
        {
            try
            {
                string token = HttpContext.Request.Cookies["token"]["valor"];
                Response.Cookies.Remove("token");
                GateWay gateway = new GateWay();
                gateway.Token = token;
                MensajeRetorno<string> returnVal = gateway.PeticionPost<Object, string>(null, "solicitantes/logout");
                if (returnVal.State == "FAIL" || returnVal.State == null)
                    ViewBag.Error = returnVal.Message;
                return Redirect("Login");
            }
            catch
            {
                return View("~/Views/Shared/_Error.cshtml");
            }
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            try
            {
                LoginSolicitante login = new LoginSolicitante();
                login.Usuario = collection["Usuario"];
                login.Clave = collection["Clave"];
                GateWay gateway = new GateWay();
                MensajeRetorno<Tokens> returnVal = returnVal = gateway.PeticionPost<LoginSolicitante,Tokens>(login,"solicitantes/Login");
                if (returnVal.State == "FAIL")
                {
                    ViewBag.Error = returnVal.Message;
                    return View();
                }
                else
                {
                    Tokens token = returnVal.Data;
                    var cookie = new HttpCookie("token");
                    cookie["valor"] = token.Token;
                    cookie["cedula"] = login.Usuario;
                    cookie.Expires = DateTime.Now.AddMinutes(30);
                    Response.Cookies.Set(cookie);
                }

                return  RedirectToAction("index","solicitudes");
            }
            catch
            {
                return View("~/Views/Shared/_Error.cshtml");
            }
        }
        [LoginFilter]   
        // GET: Solicitante/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Solicitante/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Solicitante solicitante = new Solicitante();
                solicitante.Cedula = collection["Cedula"];
                solicitante.Clave = collection["Clave"];
                solicitante.CorreoElectronico = collection["CorreoElectronico"];
                solicitante.NombreCompleto = "";
                GateWay gateway = new GateWay();
                MensajeRetorno<Solicitante> returnVal = gateway.PeticionPost<Solicitante,Solicitante>(solicitante,"solicitantes/crear");
                if (returnVal.State == "FAIL" || returnVal.State == null)
                {
                    Danger(string.Format(returnVal.Message), true);
                    return View();
                }
                Success(string.Format("<b>{0}</b> Su Cuenta fue creada exitosamente.", 
                    solicitante.Cedula), true);
                return View("Login");
            }
            catch
            {
                return View("~/Views/Shared/_Error.cshtml");
            }
        }
        [HttpGet]
        public string  NombreCompleto(string id )
        {
            Solicitante soli = new Solicitante();
            GateWay gateway = new GateWay();
            var  result = gateway.PeticionGet<Solicitante>(id, "solicitantes/GetNoAuth");
            if (result.State == "FAIL")
                return "Cedula no existe.";
            return result.Data.NombreCompleto;
        }

    }
}
