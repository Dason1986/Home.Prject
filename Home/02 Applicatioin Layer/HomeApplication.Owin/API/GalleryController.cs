using Home.DomainModel.Repositories;
using HomeApplication.Dtos;
using HomeApplication.Services;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace HomeApplication.Owin.API
{
   // [RoutePrefix("api/Gallery")]
    public class GalleryController : WebAPI
    {
        private readonly IGalleryService _service;

        public GalleryController(IGalleryService service)
        {
            _service = service;
        }
        [Route("api/Gallery/Albums")]
        public IEnumerable<GalleryType> GetAlbums()
        {
            return _service.GetAlbums();
        }
        [Route("api/Gallery/Make")]
        public IEnumerable<GalleryType> GetMake()
        {
            return _service.GetEquipmentMake();
        }
        [Route("api/Gallery/Raw")]
        public IEnumerable<GalleryType> GetRaw()
        {
            return _service.GetRawFormat();
        }
        [Route("api/Gallery/Make/{make}")]
        public IEnumerable<GalleryType> GetByMake(string make)
        {
            return _service.GetEquipmentMake(make);
        }

      //  [ActionName("model")]
        [Route("api/Gallery/model")]
        public IEnumerable<GalleryType> GetByModel()
        {
            return _service.GetEquipmentModel();
        }

     
        [Route("api/Gallery/TimeLine/year/{filter}")]
        public IEnumerable<GalleryType> GetTimeLineByYear( string filter)
        {
            return _service.GetTimeLineByformat(TimeFormat.YYYY, filter);
        }

        [Route("api/Gallery/TimeLine/yearMM")]
        public IEnumerable<GalleryType> GetTimeLineMonthByYear()
        {
            return _service.GetTimeLineByformat(TimeFormat.YYYYMM);
        }

        [Route("api/Gallery/TimeLine/year")]
        public IEnumerable<GalleryType> GetTimeLineByYYYY()
        {
            return _service.GetTimeLineByformat(TimeFormat.YYYY);
        }

        [Route("api/Gallery/TimeLine/yearmmdd")]
        public IEnumerable<GalleryType> GetTimeLineByYYYYMMDD()
        {
            return _service.GetTimeLineByformat(TimeFormat.YYYYMMDD);
        }

        [Route("api/Gallery/TimeLine/yearmmdd/{filter}")]
        public IEnumerable<GalleryType> GetTimeLineByYYYYMM( string filter)
        {
            return _service.GetTimeLineByformat(TimeFormat.YYYYMM, filter);
        }



        [Route("api/Gallery/TimeLine/yearmmdd/{filter}")]
        public IEnumerable<GalleryType> GetTimeLineByYYYYMMDD(string filter)
        {
            return _service.GetTimeLineByformat(TimeFormat.YYYYMMDD, filter);
        }

       
        [HttpGet]
        [Route("api/Gallery/Photo")]
        public HttpResponseMessage Get()
        {
            var photoinfo = _service.GetRandomPhoto();
            if (photoinfo == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "圖片不存在！");
            }
            HttpResponseMessage result = dwonimagefile(photoinfo);
            return result;
        }


        [HttpGet]
        [Route("api/Gallery/Photo/Exif/{serialNumber}")]
        public HttpResponseMessage GetExif( string serialNumber)
        {
            if (string.IsNullOrEmpty(serialNumber))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "圖片編號爲空！");
            }
            var photoinfo = _service.GetExifBySerialNumber(serialNumber);
            if (photoinfo == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "圖片編號不存在！");
            }

            return Request.CreateResponse(photoinfo);
        }


        [Route("api/Gallery/Photo/{serialNumber}")]
        public HttpResponseMessage GetPhoto(string serialNumber)
        {
            if (string.IsNullOrEmpty(serialNumber))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "圖片編號爲空！");
            }
            var photoinfo = _service.GetPhotoBySerialNumber(serialNumber);
            if (photoinfo == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "圖片編號不存在！");
            }
            HttpResponseMessage result = dwonimagefile(photoinfo);

            return result;
        }

        private static HttpResponseMessage dwonimagefile(FileProfile photoinfo)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(photoinfo.FileBuffer),
            };   
            //result.Content.Headers.ContentDisposition =
            //    new ContentDispositionHeaderValue("attachment")
            //    {
            //        FileName = photoinfo.Name,
            //        Size = photoinfo.FileBuffer.Length,
            //    };
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("image/" + photoinfo.Extension.Substring(1));
            return result;
        }
    }
}