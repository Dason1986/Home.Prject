using DomainModel.Aggregates.FileAgg;
using DomainModel.Aggregates.GalleryAgg;
using Library.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Repositories
{
    public interface IPhotoRepository : IRepository<Photo>
    {
    }

    public interface IAlbumRepository : IRepository<Album>
    {
    }

    public interface IPhtotAttributeRepository : IRepository<PhtotAttribute>
    {
    }
    public interface IFileInfoRepository : IRepository<FileInfo>
    {
        bool FileExists(string filepath);
    }
}
