using System;
using System.Collections.Generic;
using Library.Domain.Data;
using Home.DomainModel.Aggregates.GalleryAgg;
using Library.Domain.Data.Repositorys;

namespace Home.DomainModel.Repositories
{
    public interface IPhotoRepository : IRepository<Photo>
    {
        void DeletePhotoAllInfoByID(Guid iD);

        int GetAllPhotoTotal();

        IList<Photo> GetList(int beginindex, int take);

        Photo GetByFileId(Guid iD);

        Photo GetBySerialNumber(string serialNumber);
        int Count();
    }
}