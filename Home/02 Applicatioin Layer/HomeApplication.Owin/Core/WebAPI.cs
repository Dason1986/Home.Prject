using System.Web.Http;

namespace HomeApplication.Owin.API
{
    // [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-Custom-Header")]
    [CompressAttribute, ClinetLanguageAttribute, CustomExceptionFilterAttribute, RequestFilterAttribute]
    [BasicAuthenticationAttribute(BasicRealm = "Home Center")]
    public abstract class WebAPI : ApiController
    {
    }
}