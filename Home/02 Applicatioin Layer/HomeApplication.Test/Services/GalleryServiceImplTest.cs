using Autofac.Extras.Moq;
using Home.DomainModel.ModuleProviders;
using Home.DomainModel.Repositories;
using HomeApplication.Services;
using Library.Domain.Data;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeApplication.Test
{
    [TestFixture(Category = "圖像")]
    public class GalleryServiceImplTest
    {
        [Test]
        public void TestGalleryService()
        {
            AutoMap.AutoMapProfile.Reg();
            var mock = AutoMock.GetLoose();
            mock.Mock<IUnitOfWork>();
            SubRepository res = new SubRepository();

            mock.Mock<IAlbumRepository>().Setup(x => x.GetAll(Library.ComponentModel.Model.StatusCode.Enabled)).Returns(res.GetALLAlbums());
            mock.Mock<IPhotoRepository>().Setup(x => x.Get(It.IsAny<Guid>())).Returns<Guid>(x => res.GetALLPhtots().FirstOrDefault(n => n.ID == x));
            mock.Mock<IFileInfoRepository>();
            IDictionary<string, int> dictionary = new Dictionary<string, int>();
            var attres = mock.Mock<IPhotoAttributeRepository>();
            attres.Setup(x => x.GetCount(It.IsAny<string>())).Returns(0);
            attres.Setup(x => x.GetCountByValue(It.IsAny<string>(), It.IsAny<string>())).Returns<string, string>((x, y) => dictionary);
            var mokprovider = mock.Mock<IGalleryModuleProvider>();
            mokprovider.Setup(x => x.CreateFileInfo()).Returns(mock.Create<IFileInfoRepository>());
            mokprovider.Setup(x => x.CreatePhoto()).Returns(mock.Create<IPhotoRepository>());
            mokprovider.Setup(x => x.CreateAlbum()).Returns(mock.Create<IAlbumRepository>());
            mokprovider.Setup(x => x.CreatePhotoAttribute()).Returns(mock.Create<IPhotoAttributeRepository>());
            mokprovider.Setup(x => x.UnitOfWork).Returns(mock.Create<IUnitOfWork>());
            mock.Provide<IGalleryService, GalleryServiceImpl>();
            var impl = mock.Create<IGalleryService>();
            var list = impl.GetAlbums();
        }
    }
}