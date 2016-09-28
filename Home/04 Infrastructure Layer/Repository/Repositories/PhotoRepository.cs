using DomainModel.Aggregates.GalleryAgg;
using DomainModel.Repositories;
using Library.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Domain.Data.EF;

namespace Repository.Repositories
{
    public class PhotoRepository : Library.Domain.Data.EF.Repository<Photo>, IPhotoRepository
    {
        public PhotoRepository(EFContext context) : base(context)
        {
        }
    }

    public class AlbumRepository : Library.Domain.Data.EF.Repository<Album>, IAlbumRepository
    {
        public AlbumRepository(EFContext context) : base(context)
        {
        }
    }

    public class PhtotAttributeRepository : Library.Domain.Data.EF.Repository<PhtotAttribute>, IPhtotAttributeRepository
    {
        public PhtotAttributeRepository(EFContext context) : base(context)
        {
        }
    }
    public class FileInfoRepository : Library.Domain.Data.EF.Repository<DomainModel.Aggregates.FileAgg.FileInfo>, IFileInfoRepository
    {
        public FileInfoRepository(EFContext context) : base(context)
        {
        }

        public bool FileExists(string filepath)
        {
           return Set.Any(n => n.FullPath == filepath);
        }
    }
}
