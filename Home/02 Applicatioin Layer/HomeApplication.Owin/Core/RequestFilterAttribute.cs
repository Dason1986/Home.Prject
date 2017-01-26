using System;
using System.Web.Http.Controllers;

namespace HomeApplication.Owin.API
{

    public class RequestFilterAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Console.WriteLine(actionContext.Request.RequestUri);
            base.OnActionExecuting(actionContext);
        }
    }
}