using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace HomeApplication.Owin.API
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            //    if (context.Exception is NotImplementedException)
            {
                context.Response = context.Request.CreateResponse(context.Exception.Message);
            }
            Console.WriteLine(context.Exception);
        }
    }
}