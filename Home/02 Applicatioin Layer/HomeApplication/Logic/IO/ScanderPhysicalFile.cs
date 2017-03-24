using FileEx = Home.DomainModel.Aggregates.FileAgg.FileInfo;
using Home.DomainModel.ModuleProviders;
using Library.ComponentModel.Logic;
using Library.Infrastructure.Application;
using Library;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using Home.DomainModel.Repositories;

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

        private readonly IList<string> _existMd5S = new List<string>();
        private string _path;
        private string _Rootpath;

        protected override bool OnVerification()
        {
            if (string.IsNullOrEmpty(Option.Path)) throw new Exception("路徑爲空");
            _path = Path.GetFullPath(Option.Path);
            if (_path[0] == '\'' || _path[0] == '"') _path = _path.Substring(1, Option.Path.Length - 2);
            if (!Directory.Exists(_path)) Directory.CreateDirectory(_path);
            var storageEngineRepository = Bootstrap.Currnet.GetService<IStorageEngineRepository>();
            var engin = storageEngineRepository.GetByPathEngine(_path);
            if (engin == null)
            {
                engin = new Home.DomainModel.Aggregates.FileAgg.StorageEngine(CreatedInfo.ScanderPhysical)
                {
                    Root = _path,
                    Name = Path.GetDirectoryName(_path)
                };
                storageEngineRepository.Add(engin);
                storageEngineRepository.UnitOfWork.Commit();
            }
            _Rootpath = engin.Root;
            enginid = engin.ID;
            Scan(_path);
            return base.OnVerification();
        }

        private Guid enginid;

        protected override void ThreadProssSize(int beginindex, int endindex)
        {
            var take = endindex - beginindex;
            var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
            var filesRepository = provider.CreateFileInfo();
            try
            {
                foreach (var item in _files.Skip(beginindex).Take(take))
                {
                    Logger.TraceByContent("Scan file", item);
                    if (filesRepository.FileExists(item)) continue;
                    var md5 = Library.HelperUtility.FileUtility.FileMD5(item);
                    if (_existMd5S.Contains(md5))
                    {
                        DeleteFile(item);
                        continue;
                    }
                    _existMd5S.Add(md5);
                    var fileinfo = new FileEx(CreatedInfo.ScanderPhysical);
                    FileInfo sysInfo = new FileInfo(item);
                    if (_filterfile.Any(ff => sysInfo.Name.EndsWith(ff, StringComparison.OrdinalIgnoreCase))) continue;
                    fileinfo.Extension = sysInfo.Extension;
                    fileinfo.FullPath = item.Replace(_Rootpath, String.Empty);
                    fileinfo.FileName = sysInfo.Name;
                    fileinfo.MD5 = md5;
                    fileinfo.EngineID = enginid;
                    fileinfo.SourceType = Home.DomainModel.SourceType.ServerScand;

                    if (sysInfo.Exists)
                    {
                        fileinfo.FileSize = sysInfo.Length;
                    }
                    if (filesRepository.FileExists(md5, fileinfo.FileSize))
                    {
                        DeleteFile(item);

                        continue;
                    }
                    filesRepository.Add(fileinfo);
                }

                filesRepository.UnitOfWork.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void DeleteFile(string item)
        {
            try
            {
                File.Delete(item);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "文件删除失败！");
            }
        }

        private readonly string[] _filterfile = { ".DS_Store", "desktop.ini", "thumbs.db" };
        private readonly IList<string> _files = new List<string>();

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