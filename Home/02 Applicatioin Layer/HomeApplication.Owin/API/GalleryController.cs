using HomeApplication.Dtos;
using HomeApplication.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace HomeApplication.Owin.API
{
    [AllowAnonymous]
    [RoutePrefix("api/Gallery")]
    public class GalleryController : ApiController
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