using Home.DomainModel.Repositories;
using HomeApplication.Dtos;
using HomeApplication.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace HomeApplication.Owin.API
{
    [RoutePrefix("api/GalleryTimeline")]
    public class GalleryTimelineController : WebAPI
    {
        private readonly IGalleryService _service;

        public GalleryTimelineController(IGalleryService service)
        {
            _service = service;
        }

        public IEnumerable<GalleryType> Get()
        {
            return _service.GetTimeLineByformat(TimeFormat.YYYY);
        }

      
    }
}