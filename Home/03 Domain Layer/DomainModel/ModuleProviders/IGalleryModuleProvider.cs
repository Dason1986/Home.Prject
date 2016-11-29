﻿using Home.DomainModel.Repositories;
using Library.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home.DomainModel.ModuleProviders
{
    public interface IGalleryModuleProvider : IModuleProvider
    {
        IPhotoRepository CreatePhoto();

        IAlbumRepository CreateAlbum();

        IPhotoAttributeRepository CreatePhotoAttribute();

        IFileInfoRepository CreateFileInfo();

        IPhotoFingerprintRepository CreatePhotoFingerprint();

        IPhotoSimilarRepository CreatePhotoSimilar();
        IPhotoFacesRepository CreatePhotoFaces();
		ISystemParameterRepository CreateSystemParameter();
    }
}
