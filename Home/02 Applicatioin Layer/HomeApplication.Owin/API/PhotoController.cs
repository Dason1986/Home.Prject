using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;

namespace HomeApplication.Owin.API
{
	

	[AllowAnonymous]
	public class PhotoController : ApiController
	{
		[HttpGet]
		public HttpResponseMessage GetFile(string id)
		{
			var stream = new MemoryStream(Encoding.UTF8.GetBytes("hello"));
			// processing the stream.

			var result = new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new ByteArrayContent(stream.ToArray())
			};
			result.Content.Headers.ContentDisposition =
				new ContentDispositionHeaderValue("attachment")
				{
					FileName = "CertificationCard.txt"
				};
			result.Content.Headers.ContentType =
				new MediaTypeHeaderValue("application/octet-stream");

			return result;
		}

		[Route("api/Photo")]
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
