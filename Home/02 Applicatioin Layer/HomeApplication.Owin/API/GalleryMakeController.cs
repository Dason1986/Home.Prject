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

        public IEnumerable<GalleryType> Get()
        {
            return _service.GetEquipmentMake();
        }

        [ActionName("make")]
        public IEnumerable<GalleryType> GetByMake([FromUri(Name = "id")]string make)
        {
            return _service.GetEquipmentMake(make);
        }

        [ActionName("model")]
        public IEnumerable<GalleryType> GetByModel()
        {
            return _service.GetEquipmentModel();
        }
    }
}