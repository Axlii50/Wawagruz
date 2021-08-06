using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wawagruz
{
    /// <summary>
    /// this attribute allows only enter page if were redirect from server 
    /// so that means user can not manually type url and enter page without permission
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class NoDirectAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
#if(DEBUG)
            bool canAcess = false;
#else
            bool canAcess = false;
#endif

            // check the refer
            var referer = filterContext.HttpContext.Request.Headers["Referer"].ToString();

            if (!string.IsNullOrEmpty(referer))
            {
                var rUri = new System.UriBuilder(referer).Uri;
                var req = filterContext.HttpContext.Request;
                if (req.Host.Host == rUri.Host && /*req.Host.Port == rUri.Port &&*/ req.Scheme == rUri.Scheme)
                {
                    canAcess = true;
                }
                //System.Diagnostics.Debug.WriteLine(":::::"+rUri);
            }

            if (!canAcess)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index", area = "" }));
            }
        }
    }
}
