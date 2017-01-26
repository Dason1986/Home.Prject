﻿using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;
using HomeApplication.Services;

namespace HomeApplication.Owin.API
{
    [AllowAnonymous]
    [RoutePrefix("api/Photo")]
    public class PhotoController : WebAPI
    {
        private readonly IGalleryService _galleryService;

        public PhotoController(IGalleryService galleryService)
        {
            _galleryService = galleryService;
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            var photoinfo = _galleryService.GetRandomPhoto();
            if (photoinfo == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.OK, "");
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

        [ActionName("Down")]
        [HttpGet]
        public HttpResponseMessage Get([FromUri(Name = "id")]string id)
        {
            var photoinfo = _galleryService.GetPhoto(id);
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

        [HttpPost]
        public string PhotoFileUpload()
        {
            var request = HttpContext.Current.Request;

            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), request.Headers["filename"]);
            using (var fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
            {
                request.InputStream.CopyTo(fs);
            }
            return "uploaded";
        }
    }
}