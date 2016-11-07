using DomainModel.Aggregates.GalleryAgg;
using DomainModel.Repositories;
using HomeApplication.AutoMap;
using HomeApplication.Dtos;
using System;
using System.Collections.Generic;

namespace HomeApplication.Services
{

    public class GalleryServiceImpl : ServiceImpl, IGalleryService
    {
        public GalleryServiceImpl(DomainModel.ModuleProviders.IGalleryModuleProvider provider)
        {
            _provider = provider;
        }
        DomainModel.ModuleProviders.IGalleryModuleProvider _provider;
        public override string ServiceName { get { return "Gallery Service"; } }

        public int GetAllPhotoTotal()
        {
            IPhotoRepository photoRepository = _provider.CreatePhoto();
            return photoRepository.GetAllPhotoTotal();
        }

        public int GetPhotoTotalByGallery()
        {
            IPhotoRepository photoRepository = _provider.CreatePhoto();
            return photoRepository.GetAllPhotoTotal();
        }

        public IList<AlbumDto> GetAlbums()
        {
            var repository = _provider.CreateAlbum();
            var list = repository.GetAll(); 
            return list.AsList<AlbumDto>();
        }
    }
}