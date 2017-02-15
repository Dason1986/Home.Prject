using Home.DomainModel.Aggregates.GalleryAgg;
using Home.DomainModel.Repositories;
using HomeApplication.AutoMap;
using HomeApplication.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using HomeApplication.DomainServices;
using Library.HelperUtility;

namespace HomeApplication.Services
{
    public class GalleryServiceImpl : ServiceImpl, IGalleryService
    {
        public GalleryServiceImpl(Home.DomainModel.ModuleProviders.IGalleryModuleProvider provider)
        {
            _provider = provider;
        }

        private readonly Home.DomainModel.ModuleProviders.IGalleryModuleProvider _provider;
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

        private IList<GalleryType> Get(string key)
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

        public FileProfile GetRandomPhoto()
        {
            var photoRepository = _provider.CreatePhoto();
            int count = photoRepository.Count();
            if (count == 0) return null;
            int live = 0;

            Random random = new Random((int)DateTime.Now.Ticks);
            var number = random.Next(0, count);
            var photoinfos = photoRepository.GetAll().OrderBy(n => n.Created).Skip(number).Take(1);
            var photoinfo = photoinfos.First();
            if (photoinfo == null) return null;
            IPhotoEnvironment photoEnvironment = new PhotoEnvironment();
            photoEnvironment.LoadConfig(_provider.CreateSystemParameter());
            var imageStorage = photoEnvironment.CreateImageStorage(photoinfo.ID);
            var itemImageLevel = photoinfo.Attributes.FirstOrDefault(n => n.AttKey == "ImageLevel");
            if (itemImageLevel != null)
                int.TryParse(itemImageLevel.AttValue, out live);
            var fs = imageStorage.Get(live);
            FileProfile file = new FileProfile
            {
                Name = photoinfo.File.FileName,
                FileBuffer = fs.ToArray(),
                Extension = photoinfo.File.Extension,
            };
            fs.Dispose();
            return file;
        }

        public FileProfile GetPhotoBySerialNumber(string serialNumber)
        {
            var photoRepository = _provider.CreatePhoto();
            var photo = photoRepository.GetBySerialNumber(serialNumber);
            if (photo == null) return null;
            FileProfile file = new FileProfile
            {
                Name = photo.File.FileName,
                FileBuffer = File.ReadAllBytes(photo.File.FullPath),
                Extension = photo.File.Extension,
            };
            return file;
        }

        public FileProfile GetPhoto(Guid id)
        {
            var photoRepository = _provider.CreatePhoto();
            var photo = photoRepository.Get(id);
            if (photo == null) return null;
            FileProfile file = new FileProfile
            {
                Name = photo.File.FileName,
                FileBuffer = File.ReadAllBytes(photo.File.FullPath),
                Extension = photo.File.Extension,
            };
            return file;
        }

        public IDictionary<string, string> GetExifBySerialNumber(string serialNumber)
        {
            var photoRepository = _provider.CreatePhoto();
            var photo = photoRepository.GetBySerialNumber(serialNumber);
            if (photo == null) return null;
            var dic = photo.Attributes.ToDictionary(n => n.AttKey, n => n.AttValue);

            return dic;
        }
    }
}