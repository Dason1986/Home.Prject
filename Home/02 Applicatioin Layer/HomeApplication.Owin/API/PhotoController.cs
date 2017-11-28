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
    

  //  [RoutePrefix("api/UploadFile")]
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