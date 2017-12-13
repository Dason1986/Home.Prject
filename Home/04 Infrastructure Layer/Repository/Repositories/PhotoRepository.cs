using System;
using Home.DomainModel.Aggregates.GalleryAgg;
using Home.DomainModel.Repositories;
using Library.Domain.Data.EF;
using System.Linq;
using Library.ComponentModel.Model;
using System.Collections.Generic;
using System.Data.Entity;

namespace Home.Repository.Repositories
{
    public class PhotoRepository : Repository<Photo>, IPhotoRepository
    {
        public PhotoRepository(DbContext context) : base(context)
        {
        }

        public int Count()
        {
            return Wrapper.Find().Count();
        }

        public void DeletePhotoAllInfoByID(Guid id)
        {
          
            ExecuteCommand(string.Format("delete from photoattribute  where Photoid='{0}'", id));

            ExecuteCommand(string.Format("delete from photofingerprint  where Photoid='{0}'", id));
            ExecuteCommand(string.Format("delete from photosimilar  where RightPhotoID='{0}'", id));
            ExecuteCommand(string.Format("delete from photosimilar  where LeftPhotoID='{0}'", id));

            ExecuteCommand(string.Format("delete from photo  where id='{0}'", id));
        }

        public int GetAllPhotoTotal()
        {
            return GetAll().Count();
        }

        public Photo GetByFileId(Guid id)
        {
            return Wrapper.Find().FirstOrDefault(n => n.FileID == id);
        }

        public Photo GetBySerialNumber(string serialNumber)
        {
            return Wrapper.Find().FirstOrDefault(n => n.Attributes.AsQueryable().Any(ff => ff.AttKey == "SerialNumber" && ff.AttValue == serialNumber));
        }

        public IList<Photo> GetList(int beginindex, int take)
        {
            var photos = Wrapper.Find().Include("File").Include("ParentAlbum").Include("Attributes").AsNoTracking().OrderBy(n => n.Created).Skip(beginindex).Take(take).ToList();
            return photos;
        }
    }
}