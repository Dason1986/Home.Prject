using Home.DomainModel.ModuleProviders;
using Home.DomainModel.Repositories;
using Library;
using Library.ComponentModel.Logic;
using Library.Infrastructure.Application;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeApplication.Logic.IO
{
    public struct ScanderPhysicalFileOption : IOption
    {
        public string Path { get; set; }
    }

    public class ScanderPhysicalFile : BaseMultiThreadingLogicService
    {
        public ScanderPhysicalFileOption Option
        {
            get { return _option; }
            set
            {
                _option = value;
            }
        }

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

        private ScanderPhysicalFileOption _option;
        private string path;
        private IGalleryModuleProvider provider;
        private IFileInfoRepository _filesRepository;

        private int batchCount = 50;

        protected override bool OnVerification()
        {
            if (string.IsNullOrEmpty(Option.Path)) throw new Exception("路徑爲空");
            path = Option.Path;
            if (path[0] == '\'' || path[0] == '"') path = path.Substring(1, Option.Path.Length - 2);
            if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);
            provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
            _filesRepository = provider.CreateFileInfo();
            Scan(path);
            return base.OnVerification();
        }

        protected override void ThreadProssSize(int beginindex, int endindex)
        {
            var take = endindex - beginindex;
            foreach (var item in files.Skip(beginindex).Take(take))
            {
                Logger.TraceByContent("Scan file", item);
                if (_filesRepository.FileExists(item)) continue;
                var fileinfo = new Home.DomainModel.Aggregates.FileAgg.FileInfo(CreatedInfo.ScanderPhysical);
                System.IO.FileInfo sysInfo = new System.IO.FileInfo(item);
                if (filterfile.Any(ff => sysInfo.Name.EndsWith(ff, StringComparison.OrdinalIgnoreCase))) continue;
                fileinfo.Extension = sysInfo.Extension;
                fileinfo.FullPath = item;
                fileinfo.FileName = sysInfo.Name;
                fileinfo.MD5 = Library.HelperUtility.FileUtility.FileMD5(item);
                if (sysInfo.Exists) fileinfo.FileSize = sysInfo.Length;
                if (_filesRepository.FileExists(fileinfo.MD5, fileinfo.FileSize))
                {
                    try
                    {
                        System.IO.File.Delete(item);
                    }
                    catch (Exception ex)
                    {
                        Logger.WarnByContent("文件删除失败！", item);
                    }

                    continue;
                }
                _filesRepository.Add(fileinfo);
            }

            _filesRepository.UnitOfWork.Commit();
        }

        private string[] filterfile = { ".DS_Store", "desktop.ini", "thumbs.db" };
        private IList<string> files = new List<string>();

        private void Scan(string dic)
        {
            var tmpfiles = System.IO.Directory.EnumerateFiles(dic);

            foreach (var item in tmpfiles)
            {
                this.files.Add(item);
            }

            var dirs = System.IO.Directory.EnumerateDirectories(dic);
            foreach (var item in dirs)
            {
                Scan(item);
            }
        }

        protected override int GetTotalRecord()
        {
            return files.Count;
        }
    }
}