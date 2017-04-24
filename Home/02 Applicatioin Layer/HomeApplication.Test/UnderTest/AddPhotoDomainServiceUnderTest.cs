using System;
using Home.DomainModel.DomainServices;
using Home.DomainModel.ModuleProviders;
using HomeApplication.DomainServices;
using Library.Domain.DomainEvents;
using NUnit.Framework;

namespace HomeApplication.Test
{
    class AddPhotoDomainServiceUnderTest
    {
       
        private readonly IAddPhotoDomainService _domainService;
        private readonly IDomainEventArgs _args;
        IGalleryModuleProvider _moduleProvider;

        public AddPhotoDomainServiceUnderTest(IGalleryModuleProvider moduleProvider,  IAddPhotoDomainService domainService, IDomainEventArgs args)
        {
            _moduleProvider = moduleProvider;
            this._domainService = domainService;
            _args = args;
        }
        public void ArgumentNull()
        {
            var ex = Assert.Throws(typeof(PhotoDomainServiceException), () =>
            {
                _domainService.ModuleProvider = _moduleProvider;
                _domainService.Handle(_args);
            });
            Assert.That(ex.Message, Is.EqualTo(Resources.DomainServiceResource.PhotoItemArgumentNull));
        }
        public void IDIsEmpty()
        {
            var ex = Assert.Throws(typeof(PhotoDomainServiceException), () =>
           {
               _domainService.ModuleProvider = _moduleProvider;
               _domainService.Handle(_args);
           });
            Assert.That(ex.Message, Is.EqualTo(Resources.DomainServiceResource.PhotoItemArgsNull));
        }

        public void ModuleProviderIsEmpty()
        {
            var ex = Assert.Throws(typeof(PhotoDomainServiceException), () =>
            {
                _domainService.ModuleProvider = _moduleProvider;
                _domainService.Handle(_args);
            });
            Assert.That(ex.Message, Is.EqualTo(Resources.DomainServiceResource.ModuleProviderNull));
        }

        public void FileInfoNotExist()
        {
            var ex = Assert.Throws(typeof(PhotoDomainServiceException), () =>
            {
                _domainService.ModuleProvider = _moduleProvider;
                _domainService.Handle(_args);
            });
            Assert.That(ex.Message, Is.EqualTo(Resources.DomainServiceResource.FileInfoNotExist));
        }
        public void FileNotExist()
        {
            var ex = Assert.Throws(typeof(ArgumentNullException), () =>
            {
                _domainService.ModuleProvider = _moduleProvider;
                _domainService.Handle(_args);
            });
            Assert.That(ex.Message, Is.EqualTo("Value cannot be null.\r\nParameter name: Engine"));
        }
    }
}