using Home.DomainModel.Repositories;
using HomeApplication.Dtos;
using HomeApplication.Services;
using System.Collections.Generic;
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

      
    }
}