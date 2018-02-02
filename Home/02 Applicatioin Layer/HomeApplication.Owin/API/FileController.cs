using Home.DomainModel.Repositories;
using HomeApplication.Dtos;
using HomeApplication.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HomeApplication.Owin.API
{

    public class FileController : WebAPI
    {
        private readonly FileManagementServiceImpl _service;

        public FileController(FileManagementServiceImpl service)
        {
            _service = service;
        }
        [Route("api/File")]
        public IEnumerable<GalleryType> Get()
        {
            return _service.GetExtension();
        }

        private HttpResponseMessage DownFile(string fName, byte[] buffer)
        {

            var ranges = Request.Headers.GetValues("Range");
            HttpResponseMessage responseMessage;


            if (null == ranges && ranges.Any())
            { //正常情况 200
                responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                responseMessage.Content = new ByteArrayContent(buffer);
                responseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application /x-zip-compressed");
                responseMessage.Content.Headers.Add("Content-Disposition", "attachment;filename=" + fName);

                responseMessage.Content.Headers.Add("Accept-Ranges", buffer.ToString());
                responseMessage.Content = new ByteArrayContent(buffer);
            }
            else
            { //Accept - Range 情况
                var range = ranges.First();
                string[] re = range.Split('=');
                string[] r = re[1].Split('-');
                long start = 0;
                long alength = 0;
                long fslength = buffer.LongLength;
                // 如果开始为空 从0开始取
                if (string.IsNullOrEmpty(r[0]))
                    start = 0;
                else
                    start = Convert.ToInt64(r[0]);
                //取文件总长度

                // 字节长度 
                if (string.IsNullOrEmpty(r[1]))
                    alength = fslength - start;
                else
                    alength = Convert.ToInt64(r[1]) - start;
                responseMessage = new HttpResponseMessage(HttpStatusCode.PartialContent);
                responseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application /x-zip-compressed");
                responseMessage.Content.Headers.Add("Content-Disposition", "attachment;filename=" + fName);

                responseMessage.Content.Headers.Add("Accept-Ranges", (alength - start).ToString());
                responseMessage.Content.Headers.Add("Content-Range", "bytes " + start + "-" + (start + alength) + "/" + fslength.ToString());
                responseMessage.Content = new ByteArrayContent(buffer, (int)start, (int)alength);


            }
            return responseMessage;
        }
    }
}