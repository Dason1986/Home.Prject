using System;
using Home.DomainModel.Aggregates.GalleryAgg;
using Home.DomainModel.Repositories;
using Library.Domain.Data.EF;
using System.Linq;
using Library.ComponentModel.Model;
using System.Collections.Generic;

namespace Home.Repository.Repositories
{
    public class PhotoRepository : Repository<Photo>, IPhotoRepository
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

        public int GetAllPhotoTotal()
        {
            return GetAll().Where(n => n.StatusCode == StatusCode.Enabled).Count();
        }

		public Photo GetByFileId(Guid id)
		{
			return CreateSet().FirstOrDefault(n => n.FileID == id);
		}

		public IList<Photo> GetList(int beginindex, int take)
        {
            var photos = CreateSet().Include("File").Include("ParentAlbum").Include("Attributes").AsNoTracking().OrderBy(n => n.Created).Skip(beginindex).Take(take).ToList();
            return photos;
        }
    }
}
