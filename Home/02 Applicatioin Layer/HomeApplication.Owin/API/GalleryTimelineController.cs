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

        [ActionName("year")]
        public IEnumerable<GalleryType> GetTimeLineByYear([FromUri(Name = "filter")]string filter)
        {
            return _service.GetTimeLineByformat(TimeFormat.YYYY, filter);
        }

        [ActionName("yyyymm")]
        public IEnumerable<GalleryType> GetTimeLineMonthByYear()
        {
            return _service.GetTimeLineByformat(TimeFormat.YYYYMM);
        }

        [ActionName("yyyy")]
        public IEnumerable<GalleryType> GetTimeLineByYYYY()
        {
            return _service.GetTimeLineByformat(TimeFormat.YYYY);
        }

        [ActionName("yyyymmdd")]
        public IEnumerable<GalleryType> GetTimeLineByYYYYMMDD()
        {
            return _service.GetTimeLineByformat(TimeFormat.YYYYMMDD);
        }

        [ActionName("yyyymm")]
        public IEnumerable<GalleryType> GetTimeLineByYYYYMM([FromUri(Name = "filter")]string filter)
        {
            return _service.GetTimeLineByformat(TimeFormat.YYYYMM, filter);
        }

        [ActionName("yyyy")]
        public IEnumerable<GalleryType> GetTimeLineByYYYY([FromUri(Name = "filter")]string filter)
        {
            return _service.GetTimeLineByformat(TimeFormat.YYYY, filter);
        }

        [ActionName("yyyymmdd")]
        public IEnumerable<GalleryType> GetTimeLineByYYYYMMDD([FromUri(Name = "filter")]string filter)
        {
            return _service.GetTimeLineByformat(TimeFormat.YYYYMMDD, filter);
        }
    }
}