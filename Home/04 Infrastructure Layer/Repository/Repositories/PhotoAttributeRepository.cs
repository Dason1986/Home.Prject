using DomainModel.Aggregates.GalleryAgg;
using DomainModel.Repositories;
using Library.Domain.Data.EF;

namespace Repository.Repositories
{

    public class PhotoAttributeRepository : Repository<PhotoAttribute>, IPhotoAttributeRepository
    {
        public PhotoAttributeRepository(EFContext context) : base(context)
        {
        }
    }
}