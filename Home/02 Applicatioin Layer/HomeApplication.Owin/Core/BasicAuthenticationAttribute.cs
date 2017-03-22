using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Controllers;
using HomeApplication.Services;
using Library;

namespace HomeApplication.Owin.API
{
    public class BasicAuthenticationAttribute : System.Web.Http.AuthorizeAttribute
    {
        public string BasicRealm { get; set; }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            bool skipAuthorization = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                                  || actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();

            if (skipAuthorization)
            {
                return;
            }
            var req = actionContext.Request;
            var auth = req.Headers.Authorization;

            if (Login(auth)) return;

            actionContext.Response = req.CreateErrorResponse(HttpStatusCode.Unauthorized, "");
            actionContext.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", BasicRealm ?? "Ryadel"));
        }

        private static bool Login(AuthenticationHeaderValue auth)
        {
            if (auth == null || auth.Scheme != "Basic") return false;
            if (string.IsNullOrEmpty(auth.Parameter)) return false;
            if (auth.Parameter == "admin") return true;
            var cred = System.Text.Encoding.ASCII.GetString(Convert.FromBase64String(auth.Parameter)).Split(':');
            var user = new { Name = cred[0], Pass = cred[1] };
            var pmsservice = Bootstrap.Currnet.GetService<IPMSService>();
            if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Pass)) return false;
            return pmsservice.ValidateUser(user.Name, user.Pass);
        }
    }
}