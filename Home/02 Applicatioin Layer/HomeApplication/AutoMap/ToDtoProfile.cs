using Home.DomainModel.Aggregates.GalleryAgg;
using HomeApplication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApplication.AutoMap
{
    internal class ToDtoProfile : AutoMapper.Profile
    {
        public ToDtoProfile()
        {
            CreateMap<Album, AlbumDto>();
            CreateMap<PhotoAttribute, PhotoAttributeDto>();
        }
    }
}