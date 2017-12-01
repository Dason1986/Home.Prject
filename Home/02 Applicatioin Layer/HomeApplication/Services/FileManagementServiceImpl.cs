using Home.DomainModel.Aggregates.FileAgg;
using Home.DomainModel.Repositories;
using HomeApplication.Dtos;
using System;
using System.Linq;
using System.Collections.Generic;

namespace HomeApplication.Services
{

    public class FileManagementServiceImpl : ServiceImpl
    {
        public override string ServiceName { get { return "File Management Service"; } }
        protected readonly IFileInfoRepository FileInfoRepository;
        public FileManagementServiceImpl(IFileInfoRepository fileInfoRepository)
        {
            FileInfoRepository = fileInfoRepository;

        }

        public GalleryType[] GetExtension()
        {
            var dic = FileInfoRepository.GetExtension();
            return dic.Select(n => new GalleryType { Name = n.Key, Count = n.Value }).ToArray();
        }
    }
}