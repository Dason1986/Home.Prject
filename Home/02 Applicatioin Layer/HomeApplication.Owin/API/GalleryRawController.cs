using HomeApplication.Dtos;
using HomeApplication.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace HomeApplication.Owin.API
{
    [AllowAnonymous]
    [RoutePrefix("api/GalleryRaw")]
    public class GalleryRawController : ApiController
    {
        IGalleryService _service;

        public GalleryRawController(IGalleryService service)
        {
            _service = service;

        }

        public IEnumerable<GalleryType> Get()
        {



            return _service.GetRawFormat();

        }
    }
}