using HomeApplication.Cores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Hosting;

namespace HomeApplication.Owin.Core
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class JWTAuthFilterAttribute : AuthorizationFilterAttribute
    {
        public JWTAuthFilterAttribute()
        {

        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext == null) throw new Exception("actionContext");

            // 过滤带有AllowAnonymous
            if (SkipAuthorization(actionContext)) return;

            var request = ((actionContext.Request.Properties[HttpPropertyKeys.RequestContextKey] as HttpWebRequest));

            // 分别从Query、Body、Header获取Token字符串
            const string key = "api_key";
            const string querykey = "Authorization";
            string token = actionContext.Request.Headers.Authorization != null ? actionContext.Request.Headers.Authorization.Parameter : string.Empty;

            // header
            if (actionContext.Request.Headers.Contains(key) && String.IsNullOrEmpty(token))
                token = actionContext.Request.Headers.GetValues(key).First();
            // query
            var querys = actionContext.Request.GetQueryNameValuePairs();
            if ( String.IsNullOrEmpty(token)&& querys.Any(n=>n.Key.Equals(querykey)))
                token = querys.FirstOrDefault(n => n.Key.Equals(querykey)).Value;
            // body

            if (String.IsNullOrEmpty(token))
            {
                actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(
              HttpStatusCode.Unauthorized, "请先登录。");
                return;
            }
            try
            {
                var payload = new JWT().Decode<JWTPayload>(token, PassportConfig.SecretKey);
                // 检查token是否已经过期
                if (payload.Exp < DateTime.UtcNow)
                    actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(
                        HttpStatusCode.Unauthorized, "Token已经过期。");
                actionContext.Request.Headers.Add("StaffNo", payload.Name);
            }
            catch (Exception ex)
            {
                actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(
                    HttpStatusCode.Forbidden, ex.Message);
            }
        }

        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                   || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }

    }
}
