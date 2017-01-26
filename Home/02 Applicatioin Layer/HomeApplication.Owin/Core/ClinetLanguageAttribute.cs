using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace HomeApplication.Owin.API
{

    public class ClinetLanguageAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var languages = actionContext.Request.Headers.AcceptLanguage;
            if (languages == null || languages.Count == 0) return;
        }
    }
}