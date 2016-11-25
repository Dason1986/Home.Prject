using DomainModel.Aggregates.GalleryAgg;
using DomainModel.Repositories;
using HomeApplication.AutoMap;
using HomeApplication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

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



        public IList<GalleryType> GetAlbums()
        {
            var attres = _provider.CreatePhotoAttribute();
            List<GalleryType> list = new List<GalleryType>()
            {
            new GalleryType { Name = "TimeLine", Count = attres.GetCount("DateTimeDigitized") },
            new GalleryType { Name = "EquipmentMake", Count =Get("EquipmentMake").Count },
            new GalleryType { Name = "RawFormat", Count = Get("RawFormat").Count },
            new GalleryType { Name = "Selfie", Count = attres.GetCount("Selfie") }
            };
            return list;
        }


        public IList<GalleryType> GetTimeLineByformat(TimeFormat format, string filtertime = null)
        {
            var attres = _provider.CreatePhotoAttribute().GetTimeLineByformat(format, filtertime);
            List<GalleryType> list = new List<GalleryType>();
            foreach (var item in attres)
            {
                list.Add(new GalleryType { Name = item.Key, Count = item.Value });
            }

            return list;
        }
        public IList<GalleryType> GetTimeLineMonthByYear(string year)
        {
            var attres = _provider.CreatePhotoAttribute().GetTimeLineMonthByYear(year);
            List<GalleryType> list = new List<GalleryType>();
            foreach (var item in attres)
            {
                list.Add(new GalleryType { Name = item.Key, Count = item.Value });
            }
            return list;
        }
        //public IList<GalleryType> GetEquipmentMakeModel(string make)
        //{
        //    var attres = _provider.CreatePhotoAttribute();
        //    var dic = attres.GetCountByValue("EquipmentMake", make);
        //    List<GalleryType> list = new List<GalleryType>();
        //    foreach (var item in dic)
        //    {
        //        list.Add(new GalleryType { Name = item.Key, Count = item.Value });
        //    }

        //    return list;
        //}
        public IList<GalleryType> GetEquipmentMake(string make = null)
        {

            return Get("EquipmentMake");
        }
        public IList<GalleryType> GetEquipmentModel()
        {
            var attres = _provider.CreatePhotoAttribute();
            var dic = attres.GetModelCountByMake();
            List<GalleryType> list = new List<GalleryType>();
            foreach (var item in dic)
            {
                list.Add(new GalleryType { Name = item.Key, Count = item.Value });
            }

            return list;
        }
        IList<GalleryType> Get(string key)
        {
            var attres = _provider.CreatePhotoAttribute();
            var dic = attres.GetCountByValue(key);
            List<GalleryType> list = new List<GalleryType>();
            foreach (var item in dic)
            {
                list.Add(new GalleryType { Name = item.Key, Count = item.Value });
            }

            return list;
        }
        public IList<GalleryType> GetRawFormat()
        {

            return Get("RawFormat");
        }
    }
}