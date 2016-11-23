using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace HomeApplication.Owin.API
{
	[AllowAnonymous]
	public class GalleryController : ApiController
	{
		public GalleryController()
		{

		}
		[HttpGet]
		public IEnumerable<GalleryType> GetAllGallery()
		{

			yield return new GalleryType { Name = "TimeLine", Count = 11 };
			yield return new GalleryType { Name = "EquipmentMake", Count = 12 };
			yield return new GalleryType { Name = "RawFormat", Count = 12 };
		}
	}
	public class GalleryType
	{

		public String Name
		{
			get;
			set;
		}
		public int Count
		{
			get;
			set;
		}
	}

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
