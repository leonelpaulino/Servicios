using Servicios.Filter;
using Servicios.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Servicios.Controllers
{
    [AuthenticationFilter]
    public class SolicitudesController : BaseController
    {
        // GET: Solicitudes
        public ActionResult Index()
        {
            try
            {
                GateWay gateway = new GateWay();
                gateway.Token = Request.Cookies["token"]["valor"];
                Solicitante usuario = Usuario();
                ViewBag.nombreUsuario = usuario.NombreCompleto;
                var result = gateway.PeticionGet<IEnumerable<Solicitudes>>(usuario.Id.ToString(), "Solicitudes/collection"); 
                if (result.Data == null && result.State=="SUCCESS")
                    return View(new List<Solicitudes>());
                else if (result.State == "FAIL")
                    return View("~/Views/Shared/_Error.cshtml");
                return View(result.Data.ToList());
            }
            catch {
                return View("~/Views/Shared/_Error.cshtml");
            }
        }

        // GET: Solicitudes/Details
        public ActionResult Details(string id )
        {
            try
            {
                GateWay gateway = new GateWay();
                gateway.Token = Request.Cookies["token"]["valor"];
                Solicitante usuario = Usuario();
                ViewBag.nombreUsuario = usuario.NombreCompleto;
                var result = gateway.PeticionGet<Solicitudes>(id, "solicitudes/Details");
                if (result.State == "FAIL")
                    return View("Error");
                var estado = gateway.PeticionGet<IEnumerable<Estado>>(result.Data.Servicios.Id.ToString(), "estados/collection");
                if (estado.State == "FAIL")
                    return View("Error");
                ViewBag.estados = estado.Data.OrderBy(est => est.Posicion).ToList();
                ViewBag.solicitud = result.Data;
                return View(result.Data);
            }
            catch {
                return View("~/Views/Shared/_Error.cshtml");
            }
        }
        private Solicitante Usuario()
        {
                GateWay gateway = new GateWay();
                gateway.Token = Request.Cookies["token"]["valor"];
                var solicitante = gateway.PeticionGet<Solicitante>(Request.Cookies["token"]["cedula"].ToString(), "solicitantes/get");
                if (solicitante.State == "FAIL")
                    return null;
                return solicitante.Data;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Create(string id)
        {
            try {
                if (ViewBag.Error != null)
                    ViewBag.Error = "";
                Solicitante usuario = Usuario();
                if (usuario == null)
                    return RedirectToAction("Login", "Solicitante");
                GateWay gateway = new GateWay();
                gateway.Token = Request.Cookies["token"]["valor"];
                ViewBag.nombreUsuario = usuario.NombreCompleto;
                var servicios = gateway.PeticionGet<IEnumerable<Servicio>>("", "servicios/collection");
                if (servicios.State == "FAIL")
                {
                    return View("~/Views/Shared/_Error.cshtml");
                }
                servicios.Data = servicios.Data.OrderBy(servi => servi.NombreServicios);
                var estado = new MensajeRetorno<IEnumerable<Estado>>();
                if (id == null)
                    estado = gateway.PeticionGet<IEnumerable<Estado>>(servicios.Data.First().Id.ToString(), "estados/collection");
                else
                    estado = gateway.PeticionGet<IEnumerable<Estado>>(id, "estados/collection");
                if (estado.State == "FAIL")
                    return View("~/Views/Shared/_Error.cshtml");
                ViewBag.estados = estado.Data.OrderBy(est => est.Posicion).ToList();
                ViewBag.selectServicio = id == null ? servicios.Data.First() : servicios.Data.Where(ser => ser.Id.ToString() == id).FirstOrDefault();
                ViewBag.servicios = servicios.Data;
                ViewBag.solicitante = usuario;
                return View();
            }
            catch
            {
                return View("~/Views/Shared/_Error.cshtml");
            }
        }
        // POST: Solicitudes/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, HttpPostedFileBase archivo, HttpPostedFileBase archivo2)
        {
            try
            {
                List<HttpPostedFileBase> archivos = new List<HttpPostedFileBase>();
                archivos.Add(archivo);
                if ( archivo2 != null)
                    archivos.Add(archivo2);
                Solicitudes solicitud = new Solicitudes
                {
                    CorreoElectronico = collection["CorreoElectronico"],
                    Institucion = collection["Institucion"],
                    CantParticipante = Convert.ToInt32(collection["CantParticipante"]),
                    Direccion = collection["Direccion"],
                    Fecha = DateTime.Today,
                    Telefono = collection["Telefono"]
                };
                GateWay gateway = new GateWay();
                gateway.Token = Request.Cookies["token"]["valor"];
                var soli = gateway.PeticionGet<Solicitante>(Request.Cookies["token"]["cedula"].ToString(), "solicitantes/get");
                if (soli.State == "FAIL") 
                {
                    return RedirectToAction("Login","Solicitante");
                }
                var servi = gateway.PeticionGet<Servicio>(collection["Servicio"], "servicios/get");
                if (servi.State == "FAIL")
                {
                    ViewBag.Error = servi.Message;
                    return View();
                }
                solicitud.Solicitantes = soli.Data;
                solicitud.Servicios = servi.Data;

                List<Archivo> rarchivos = new List<Archivo>();
                foreach (HttpPostedFileBase i in archivos)
                {
                    using (FileStream fileStream = new FileStream(i.FileName, FileMode.Create))
                    {
                        BinaryReader b = new BinaryReader(i.InputStream);
                        byte[] fileContent = b.ReadBytes((int)i.InputStream.Length);
                        fileStream.Write(fileContent, 0, fileContent.Length);
                        rarchivos.Add(new Archivo
                        {
                            Contenido = fileContent,
                            NombreArchivo = i.FileName,
                            TipoDeArchivo = i.FileName.Substring(i.FileName.LastIndexOf('.')) == "docx"? "application/msword": "application/pdf",
                            Longitud = i.ContentLength
                        });
                           
                    }
                }
                solicitud.Archivos = rarchivos;
                var result = gateway.PeticionPost<Solicitudes, Solicitudes>(solicitud, "solicitudes/create");
                if (result.State == "FAIL")
                {
                    ViewBag.Error = result.Message;
                    return View();
                }
                Success(string.Format("<b>{0}</b> Su solicitud fue enviada exitosamente.",
                        solicitud.Servicios.NombreServicios), true);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("~/Views/Shared/_Error.cshtml");
            }
        }
        // POST: Solicitudes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View("~/Views/Shared/_Error.cshtml");
            }
        }
    }
}
