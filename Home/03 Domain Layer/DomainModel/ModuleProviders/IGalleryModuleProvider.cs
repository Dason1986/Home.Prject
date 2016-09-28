using DomainModel.Repositories;
using Library.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.ModuleProviders
{
    public interface IGalleryModuleProvider : IModuleProvider
    {
        IPhotoRepository CreatePhoto();

        IAlbumRepository CreateAlbum();

        IPhtotAttributeRepository CreatePhtotAttribute();

        IFileInfoRepository CreateFileInfo();
    }
}
