using DomainModel.Aggregates.GalleryAgg;
using HomeApplication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApplication.AutoMap
{
    class ToDtoProfile: AutoMapper.Profile
    {
        public ToDtoProfile()
        {
            CreateMap<Album, AlbumDto>();
        }
    }
}
