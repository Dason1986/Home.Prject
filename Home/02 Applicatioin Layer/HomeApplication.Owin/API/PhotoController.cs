using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace HomeApplication.Owin.API
{
    [AllowAnonymous]
    public class PhotoController : ApiController
    {
        [HttpGet]
        public string Hello(string id)
        {
            return "111:"+id;
        }
    }
}
