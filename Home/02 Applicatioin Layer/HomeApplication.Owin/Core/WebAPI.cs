using System.Web.Http;

namespace HomeApplication.Owin.API
{
    // [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-Custom-Header")]
    [CompressAttribute, ClinetLanguageAttribute, CustomExceptionFilterAttribute, RequestFilterAttribute]
    public abstract class WebAPI : ApiController
    {
    }
}