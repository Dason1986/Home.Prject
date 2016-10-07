using System;
using DomainModel.Aggregates.GalleryAgg;
using DomainModel.Repositories;
using Library.Domain.Data.EF;

namespace Repository.Repositories
{
    public class PhotoRepository : Library.Domain.Data.EF.Repository<Photo>, IPhotoRepository
    {
        public PhotoRepository(EFContext context) : base(context)
        {
        }

        public void DeletePhotoAllInfoByID(Guid id)
        {
            var cmd = this.EfContext.Database;
            cmd.ExecuteSqlCommand(string.Format("delete from photoattribute  where Photoid='{0}'", id));

            cmd.ExecuteSqlCommand(string.Format("delete from photofingerprint  where Photoid='{0}'", id));
            cmd.ExecuteSqlCommand(string.Format("delete from photosimilar  where RightPhotoID='{0}'", id));
            cmd.ExecuteSqlCommand(string.Format("delete from photosimilar  where LeftPhotoID='{0}'", id));

            cmd.ExecuteSqlCommand(string.Format("delete from photo  where id='{0}'", id));

        }
    }
}
