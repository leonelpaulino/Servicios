using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Servicios.Filter
{
    public class LoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var req = filterContext.RequestContext.HttpContext.Request;
            if (req.Cookies["token"] != null)
            {
                var token = req.Cookies["token"]["valor"];
                if (Utility.isValid(token))
                {
                    filterContext.HttpContext.Session["cedula"] = 
                    filterContext.Result = new RedirectToRouteResult(
                        new System.Web.Routing.RouteValueDictionary
                        {
                            { "controller","solicitudes"},
                            { "action","Index"}
                        }
                        );
                }

            }
        }
    }
}