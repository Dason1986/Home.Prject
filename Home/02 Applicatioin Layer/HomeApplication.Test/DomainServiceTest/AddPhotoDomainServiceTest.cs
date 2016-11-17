using Autofac;
using Autofac.Extras.Moq;
using DomainModel.Aggregates.GalleryAgg;
using DomainModel.DomainServices;
using DomainModel.ModuleProviders;
using DomainModel.Repositories;
using HomeApplication.DomainServices;
using Library;
using Library.Domain.Data;
using Library.Domain.DomainEvents;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace HomeApplication.Test
{
    [TestFixture]
    public class AddPhotoDomainServiceTest
    {

        AutoMock mock;
     
        [NUnit.Framework.SetUp]
        public void Init()
        {
            mock = AutoMock.GetLoose();

      

            SubRepository res = new SubRepository();
            res.InitPhoto();
            mock.Mock<IFileInfoRepository>();
            mock.Mock<IUnitOfWork>(); 
            mock.Mock<IPhotoRepository>().Setup(x => x.Get(It.IsAny<Guid>())).Returns<Guid>(x => res.GetALLPhtots().FirstOrDefault(n => n.ID == x));
            mock.Mock<IFileInfoRepository>().Setup(x => x.Get(It.IsAny<Guid>())).Returns<Guid>(x => res.GetALLFiles().FirstOrDefault(n => n.ID == x));
            var mokprovider = mock.Mock<IGalleryModuleProvider>();
            mokprovider.Setup(x => x.CreateFileInfo()).Returns(mock.Create<IFileInfoRepository>());
            mokprovider.Setup(x => x.CreatePhoto()).Returns(mock.Create<IPhotoRepository>());
            mokprovider.Setup(x => x.UnitOfWork).Returns(mock.Create<IUnitOfWork>());
            mock.Provide<IAddPhotoDomainService, AddPhotoDomainService>();
        }
     
        [Test]
        public void TestPhotoDomainServiceError()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo("en-us");
            {
                var service = mock.Create<AddPhotoDomainServiceUnderTest>(new TypedParameter(typeof(IDomainEventArgs), null));
                service.ArgumentNull();
            }
            {
                var service = mock.Create<AddPhotoDomainServiceUnderTest>(new TypedParameter(typeof(IDomainEventArgs), new PhotoItemEventArgs()));
                service.IDIsEmpty();
            }
            {
                var service = mock.Create<AddPhotoDomainServiceUnderTest>(
               new TypedParameter(typeof(IGalleryModuleProvider), null),
               new TypedParameter(typeof(IDomainEventArgs), new PhotoItemEventArgs(SubRepository.fileid, SubRepository.photoid))
               );
                service.ModuleProviderIsEmpty();
            }
            {
                var service = mock.Create<AddPhotoDomainServiceUnderTest>(
               new TypedParameter(typeof(IDomainEventArgs), new PhotoItemEventArgs(Guid.NewGuid(), Guid.Empty))
               );
                service.FileInfoNotExist();
            }

            {
                var service = mock.Create<AddPhotoDomainServiceUnderTest>(
               new TypedParameter(typeof(IDomainEventArgs), new PhotoItemEventArgs(SubRepository.fileid, SubRepository.photoid))
               );
                service.FileNotExist();
            }
        }
    }
}
