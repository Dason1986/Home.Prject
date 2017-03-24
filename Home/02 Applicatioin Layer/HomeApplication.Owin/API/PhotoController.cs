using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using HomeApplication.Services;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Home.DomainModel.Repositories;
using Microsoft.CSharp.RuntimeBinder;

namespace HomeApplication.Owin.API
{
    [RoutePrefix("api/Photo")]
    public class PhotoController : WebAPI
    {
        private readonly IGalleryService _galleryService;

        public PhotoController(IGalleryService galleryService)
        {
            _galleryService = galleryService;
        }

        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var photoinfo = _galleryService.GetRandomPhoto();
            if (photoinfo == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "圖片不存在！");
            }
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(photoinfo.FileBuffer),
            };
            //result.Content.LoadIntoBufferAsync(photoinfo.FileBuffer.Length).Wait();
            //result.Content.Headers.ContentDisposition =
            //    new ContentDispositionHeaderValue("attachment")
            //    {
            //        FileName = photoinfo.Name
            //    };
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("image/" + photoinfo.Extension.Substring(1));

            return result;
        }

        [ActionName("exif")]
        [HttpGet]
        public HttpResponseMessage GetExif([FromUri(Name = "id")]string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "圖片編號爲空！");
            }
            var photoinfo = _galleryService.GetExifBySerialNumber(id);
            if (photoinfo == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "圖片編號不存在！");
            }

            return Request.CreateResponse(photoinfo);
        }

        [ActionName("Down")]
        [HttpGet]
        public HttpResponseMessage Get([FromUri(Name = "id")]string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "圖片編號爲空！");
            }
            var photoinfo = _galleryService.GetPhotoBySerialNumber(id);
            if (photoinfo == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "圖片編號不存在！");
            }
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(photoinfo.FileBuffer),
            };
            //result.Content.LoadIntoBufferAsync(photoinfo.FileBuffer.Length).Wait();
            //result.Content.Headers.ContentDisposition =
            //    new ContentDispositionHeaderValue("attachment")
            //    {
            //        FileName = photoinfo.Name
            //    };
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("image/" + photoinfo.Extension.Substring(1));

            return result;
        }
    }

    [RoutePrefix("api/UploadFile")]
    public class UploadFileController : WebAPI
    {
        private readonly ISystemParameterRepository _systemParameterRepository;
        protected readonly string ServerUploadFolder;

        public UploadFileController(ISystemParameterRepository systemParameterRepository)
        {
            _systemParameterRepository = systemParameterRepository;
            _systemParameterRepository.GetListByGroup("UploadFileSetting");
            ServerUploadFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        [HttpPost]
        public void PhotoFileUpload()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            var streamProvider = new MultipartMemoryStreamProvider();
            var staffnos = GetStaffNos();
            Console.WriteLine(staffnos);
            var task = Request.Content.ReadAsMultipartAsync(streamProvider);
            task.Wait();
            foreach (HttpContent content in streamProvider.Contents)
            {
                var byta = content.ReadAsByteArrayAsync();
                byta.Wait();
                Console.WriteLine(byta.Result);
            }
        }
    }
}