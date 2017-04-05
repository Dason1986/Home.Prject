﻿using FileEx = Home.DomainModel.Aggregates.FileAgg.FileInfo;
using Home.DomainModel.ModuleProviders;
using Library.ComponentModel.Logic;
using Library.Infrastructure.Application;
using Library;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using Home.DomainModel.Aggregates.FileAgg;
using Home.DomainModel.DomainServices;
using Home.DomainModel.Repositories;
using HomeApplication.DomainServices;

namespace HomeApplication.Logic.IO
{
    public struct ScanderPhysicalFileOption : IOption
    {
        public string Path { get; set; }
    }

    public class ScanderPhysicalFile : BaseMultiThreadingLogicService
    {
        public ScanderPhysicalFileOption Option { get; set; }

        protected override IOption ServiceOption
        {
            get
            {
                return Option;
            }

            set
            {
                Option = (ScanderPhysicalFileOption)value;
            }
        }

        private string _path;

        protected override bool OnVerification()
        {
            if (string.IsNullOrEmpty(Option.Path)) throw new Exception("路徑爲空");
            _path = Path.GetFullPath(Option.Path);
            if (_path[0] == '\'' || _path[0] == '"') _path = _path.Substring(1, Option.Path.Length - 2);
            if (!Directory.Exists(_path)) Directory.CreateDirectory(_path);
            var storageEngineRepository = Bootstrap.Currnet.GetService<IStorageEngineRepository>();
            _engin = storageEngineRepository.GetByPathEngine(_path);
            if (_engin == null)
            {
                _engin = new Home.DomainModel.Aggregates.FileAgg.StorageEngine(CreatedInfo.ScanderPhysical)
                {
                    Root = _path,
                    Name = Path.GetDirectoryName(_path)
                };
                storageEngineRepository.Add(_engin);
                storageEngineRepository.UnitOfWork.Commit();
            }
            Scan(_path);
            return base.OnVerification();
        }

        protected override void ThreadProssSize(int beginindex, int endindex, int take)
        {
            try
            {
                var provider = Bootstrap.Currnet.GetService<IFileManagentModuleProvider>();

                var files = _files.Skip(beginindex).Take(take).Select(n => new System.IO.FileInfo(n)).ToArray();

                #region MyRegion

                var domainService = Bootstrap.Currnet.GetService<IAddFileDomainService>();
                domainService.ModuleProvider = provider;

                domainService.Handle(new AddFileEventArgs(_engin, files, Home.DomainModel.SourceType.ServerScand, CreatedInfo.PhotoFileAnalysis));
                domainService.ModuleProvider.UnitOfWork.Commit();

                GC.Collect();

                #endregion MyRegion
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private readonly IList<string> _files = new List<string>();
        private StorageEngine _engin;

        private void Scan(string dic)
        {
            var tmpfiles = Directory.EnumerateFiles(dic);

            foreach (var item in tmpfiles)
            {
                this._files.Add(item);
            }

            var dirs = Directory.EnumerateDirectories(dic);
            foreach (var item in dirs)
            {
                Scan(item);
            }
        }

        protected override int GetTotalRecord()
        {
            return _files.Count;
        }
    }
}