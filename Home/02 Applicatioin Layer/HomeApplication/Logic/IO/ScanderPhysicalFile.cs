using DomainModel.ModuleProviders;
using DomainModel.Repositories;
using Library;
using Library.ComponentModel.Logic;
using Library.Domain.Data.EF;
using Library.Infrastructure.Application;
using Repository;
using Repository.ModuleProviders;
using System;

namespace HomeApplication.Logic.IO
{
    public struct ScanderPhysicalFileOption : IOption
    {
        public string Path { get; set; }

    }
    public class ScanderPhysicalFileOptionCommandBuilder : IOptionCommandBuilder<ScanderPhysicalFileOption>
    {
        public ScanderPhysicalFileOption GetOption()
        {
            return _option;
        }
        ScanderPhysicalFileOption _option;
        IOption IOptionCommandBuilder.GetOption()
        {
            return _option;
        }
        public void RumCommandLine()
        {
            _option = new ScanderPhysicalFileOption();
            LabCmd:
            Console.Write("輸入指定掃描目標：");
            var path = Console.ReadLine();
            if (string.IsNullOrEmpty(path))
            {
                Console.WriteLine("不能爲空");
                goto LabCmd;
            }
            if (!System.IO.Directory.Exists(path))
            {
                Console.WriteLine("目錄不存在");
                goto LabCmd;
            }
            _option.Path = path;
        }
    }

    public class ScanderPhysicalFile : BaseLogicService
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
        ScanderPhysicalFileOption _option;
        string path;
        IGalleryModuleProvider provider;
        IFileInfoRepository _filesRepository;

        int batchCount = 50;
        protected override void OnDowrok()
        {
            Logger.Info("開始");
            if (string.IsNullOrEmpty(Option.Path)) throw new Exception("路徑爲空");
            path = Option.Path;
            if (path[0] == '\'' || path[0] == '"') path = path.Substring(1, Option.Path.Length - 2);
            if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);
            provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
            _filesRepository = provider.CreateFileInfo();
            Scan(path);

        }
        void Scan(string dic)
        {

            var files = System.IO.Directory.EnumerateFiles(dic);
            int count = 0;
            foreach (var item in files)
            {
                count++;

                Logger.Trace(item);
                if (_filesRepository.FileExists(item)) continue;
                var fileinfo = new DomainModel.Aggregates.FileAgg.FileInfo(CreatedInfo.ScanderPhysical);
                System.IO.FileInfo sysInfo = new System.IO.FileInfo(item);
                fileinfo.Extension = sysInfo.Extension;
                fileinfo.FullPath = item;
                fileinfo.FileName = sysInfo.Name;
                fileinfo.MD5 = Library.HelperUtility.FileUtility.FileMD5(item);
                if (sysInfo.Exists) fileinfo.FileSize = sysInfo.Length;
                if (_filesRepository.FileExists(fileinfo.MD5, fileinfo.FileSize)) fileinfo.StatusCode = Library.ComponentModel.Model.StatusCode.Disabled;
                _filesRepository.Add(fileinfo);
                if (count >= batchCount)
                {
                    count = 0;
                    _filesRepository.UnitOfWork.Commit();
                }
            }
            if (count != 0)
                _filesRepository.UnitOfWork.Commit();
            var dirs = System.IO.Directory.EnumerateDirectories(dic);
            foreach (var item in dirs)
            {
                Scan(item);
            }
        }






    }
}