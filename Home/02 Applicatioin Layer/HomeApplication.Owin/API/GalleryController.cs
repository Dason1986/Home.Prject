using HomeApplication.Dtos;
using HomeApplication.Services;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HomeApplication.Owin.API
{

    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-Custom-Header")]
    public abstract class WebAPI : ApiController
    {

    }
    [AllowAnonymous]
    [RoutePrefix("api/Gallery")]
    public class GalleryController : WebAPI
    {
        IGalleryService _service;

        public GalleryController(IGalleryService service)
        {
            _service = service;

        }



        public IEnumerable<GalleryType> Get()
        {

            return _service.GetAlbums();

        }

    }

}