using System.Linq;
using System.Web.Http;

namespace HomeApplication.Owin.API
{
    // [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-Custom-Header")]
    [ClinetLanguageAttribute, CustomExceptionFilterAttribute, RequestFilterAttribute]
    //[BasicAuthenticationAttribute(BasicRealm = "Home Center")]
    public abstract class WebAPI : ApiController
    {
        public WebAPI()
        {
        }

        protected string[] GetStaffNos()
        {
            var staffNos = Request.Headers.GetValues("StaffNo").ToArray();
            return staffNos;
        }
    }
}