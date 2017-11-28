using HomeApplication.Dtos;
using HomeApplication.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace HomeApplication.Owin.API
{
    [RoutePrefix("api/GalleryMake")]
    public class GalleryMakeController : WebAPI
    {
        private readonly IGalleryService _service;

        public GalleryMakeController(IGalleryService service)
        {
            _service = service;
        }

      
    }
}