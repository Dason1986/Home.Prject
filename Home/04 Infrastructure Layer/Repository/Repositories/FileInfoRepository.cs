using DomainModel.Repositories;
using System.Collections.Generic;
using System.Linq;
using Library.Domain.Data.EF;
using DomainModel.Aggregates.FileAgg;

namespace Repository.Repositories
{
    public class FileInfoRepository : Library.Domain.Data.EF.Repository<DomainModel.Aggregates.FileAgg.FileInfo>, IFileInfoRepository
    {
        public FileInfoRepository(EFContext context) : base(context)
        {
        }

        public bool FileExists(string filepath)
        {
            return Set.Any(n => n.FullPath == filepath);
        }

        public IEnumerable<FileInfo> GetFilesByExtensions(string[] extensions)
        {
            return Set.Where(n => extensions.Contains(n.Extension));
        }
    }
}