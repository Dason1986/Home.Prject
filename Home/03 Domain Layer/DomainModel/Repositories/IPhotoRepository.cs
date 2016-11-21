using System;
using System.Collections.Generic;
using DomainModel.Aggregates.GalleryAgg;
using Library.Domain.Data;

namespace DomainModel.Repositories
{
    public interface IPhotoRepository : IRepository<Photo>
    {
        void DeletePhotoAllInfoByID(Guid iD);
        int GetAllPhotoTotal();
        IList<Photo> GetList(int beginindex, int take);
		Photo GetByFileId(Guid iD);
	}
}
