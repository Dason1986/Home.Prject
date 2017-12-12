using Library;
using Library.ExceptionProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace HomeApplication.Owin.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class LogicExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(HttpActionExecutedContext context)
        {
            string logname = context.Exception.TargetSite != null && context.Exception.TargetSite.DeclaringType != null ?
                string.Format("{0} => {1}", context.Exception.TargetSite.DeclaringType.FullName, context.Exception.TargetSite.Name) :
              string.Format("{0} => {1}", context.ActionContext.ControllerContext.ControllerDescriptor.ControllerName, context.ActionContext.ActionDescriptor.ActionName)
                ;
            NLog.ILogger logger = NLog.LogManager.GetLogger(logname);
            if (context.Exception is LogicException)
            {
                var logic = context.Exception as LogicException;
                logger.Warn(context.Exception);

                context.Response = context.Request.CreateResponse<ErrorResult>(HttpStatusCode.InternalServerError, logic);
            }
            else if (CustomExceptionProvider.Providers.HandleError(context.Exception))
            {
                string mm = "";
                var fe = CustomExceptionProvider.Providers.ProvideFault(context.Exception, ref mm);

                context.Response = context.Request.CreateResponse<ErrorResult>(HttpStatusCode.InternalServerError, new ErrorResult { Message = "錯誤", Reason = mm });

                //       context.Response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, mm);
            }
            else
            {

                logger.Error(context.Exception, "未知異常");

                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "未知異常");
            }
        }
    }
    class ErrorResult
    {
        public string Message { get; set; }
        public double Code { get; set; }
        public string Reason { get; set; }

        public static implicit operator ErrorResult(LogicException error)
        {
            return new ErrorResult() { Message = error.Message, };
        }

    }
}
