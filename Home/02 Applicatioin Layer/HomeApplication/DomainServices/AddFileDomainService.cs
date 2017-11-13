using System;
using System.Collections.Generic;
using System.Linq;
using Home.DomainModel.Aggregates.FileAgg;
using Home.DomainModel.DomainServices;
using Library.ComponentModel.Model;
using Library.Domain.DomainEvents;
using Library.Infrastructure.Application;
using FileEx = Home.DomainModel.Aggregates.FileAgg.FileInfo;

namespace HomeApplication.DomainServices
{
    public class AddFileDomainService : FileManagentDomainService, IAddFileDomainService
    {
       

        private readonly IList<string> _existMd5S = new List<string>();
        private readonly string[] _filterfile = { ".DS_Store", "desktop.ini", "thumbs.db" };
        private Guid _enginid;
        private string _rootpath;

        public void Handle(AddFileEventArgs args)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (args.Engine == null) throw new ArgumentNullException(nameof(args.Engine));
            if (args.CreatedInfo == null) throw new ArgumentNullException(nameof(args.CreatedInfo));
            if (args.MemoryFiles == null && args.PhysicalFiles == null) throw new ArgumentException("沒在文件");

            _enginid = args.Engine.ID;
            _rootpath = args.Engine.Root;
            if (args.MemoryFiles != null)
            {
                Memory(args);
            }
            else if (args.PhysicalFiles != null)
            {
                Physical(args);
            }
            FilesRepository.UnitOfWork.Commit();
        }

        private void Physical(AddFileEventArgs args)
        {
            var files = args.PhysicalFiles;
            foreach (var item in files)
            {
                Logger.TraceByContent("Scan file", item.FullName);
                if (string.IsNullOrEmpty(item.FullName)) continue;
                if (_filterfile.Any(ff => item.Name.EndsWith(ff, StringComparison.OrdinalIgnoreCase))) continue;
                var filepath = item.FullName.Replace(_rootpath, string.Empty);
                if (FilesRepository.FileExists(filepath)) continue;
                var md5 = Library.HelperUtility.FileUtility.FileMD5(item.FullName);
                if (_existMd5S.Contains(md5))
                {
                    continue;
                }
                _existMd5S.Add(md5);
                var fileinfo = new FileEx(args.CreatedInfo)
                {
                    Extension = item.Extension,
                    FullPath = filepath,
                    FileName = item.Name,
                    MD5 = md5,
                    EngineID = _enginid,
                    SourceType = args.SourceType
                };

                if (item.Exists)
                {
                    fileinfo.FileSize = item.Length;
                }
                if (FilesRepository.FileExists(md5, fileinfo.FileSize))
                {
                    continue;
                }
                FilesRepository.Add(fileinfo);
            }
        }

        private void Memory(AddFileEventArgs args)
        {
            var files = args.MemoryFiles;
            foreach (var item in files)
            {
                Logger.TraceByContent("Scan file", item.Name);
                if (string.IsNullOrEmpty(item.Name)) continue;
                if (_filterfile.Any(ff => item.Name.EndsWith(ff, StringComparison.OrdinalIgnoreCase))) continue;
                var filepath = item.Name.Replace(_rootpath, string.Empty);
                if (FilesRepository.FileExists(filepath)) continue;
                var md5 = Library.HelperUtility.FileUtility.FileMD5(item.Buffer);
                if (_existMd5S.Contains(md5))
                {
                    continue;
                }
                _existMd5S.Add(md5);
                var fileinfo = new FileEx(args.CreatedInfo)
                {
                    FullPath = filepath,
                    MD5 = md5,
                    EngineID = _enginid,
                    SourceType = args.SourceType
                };
                var sysInfo = new System.IO.FileInfo(item.Name);
                fileinfo.Extension = sysInfo.Extension;
                fileinfo.FileName = sysInfo.Name;

                if (sysInfo.Exists)
                {
                    fileinfo.FileSize = sysInfo.Length;
                }
                if (FilesRepository.FileExists(md5, fileinfo.FileSize))
                {
                    continue;
                }
                FilesRepository.Add(fileinfo);
            }
        }

        protected override void Handle(DomainEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}