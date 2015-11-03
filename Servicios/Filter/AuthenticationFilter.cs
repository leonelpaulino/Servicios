using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Servicios.Filter
{
    public class AuthenticationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var req = filterContext.RequestContext.HttpContext.Request;
            if ( req.Cookies["token"] !=null)
            {
                var token = req.Cookies["token"]["valor"];
                if (Utility.isValid(token))
                {   
                    return;
                }
            }
            filterContext.Result = new RedirectToRouteResult(
                  new System.Web.Routing.RouteValueDictionary
                  {
                     { "controller","solicitante"},
                     { "action","Login"}
                  });
        }
    }
}