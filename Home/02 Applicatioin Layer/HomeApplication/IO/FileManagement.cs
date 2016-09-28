using DomainModel.Repositories;
using Library.ComponentModel.Model;
using Repository;
using Repository.ModuleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApplication.IO
{
    public class FileManagement
    {
    }
    public class CreatedInfo : ICreatedInfo
    {
        public DateTime Created
        {
            get
            {
                return DateTime.Now;
            }


        }

        public string CreatedBy { get; protected set; }

        static CreatedInfo()
        {

            ScanderPhysical = new CreatedInfo() { CreatedBy = "ScanderPhysical" };
        }
        public static CreatedInfo ScanderPhysical { get; private set; }
    }

    public class ScanderPhysicalFile
    {
        public ScanderPhysicalFileOption Option { get; set; }
        string path;
        GalleryModuleProvider provider;
        IFileInfoRepository _filesRepository;
        MainBoundedContext dbcontext;
        ICreatedInfo createinfo;
        int batchCount = 50;
        public void Run()
        {
            if (string.IsNullOrEmpty(Option.Path)) throw new Exception("路徑爲空");
            path = Option.Path.Substring(1, Option.Path.Length - 2);
            if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);
            dbcontext = new MainBoundedContext();
            provider = new GalleryModuleProvider(dbcontext);
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
                Console.WriteLine(item);
                if (_filesRepository.FileExists(item)) continue;
                var fileinfo = new DomainModel.Aggregates.FileAgg.FileInfo(CreatedInfo.ScanderPhysical);
                System.IO.FileInfo sysInfo = new System.IO.FileInfo(item);
                fileinfo.Extension = sysInfo.Extension;
                fileinfo.FullPath = item;
                fileinfo.FileName = sysInfo.Name;
                fileinfo.MD5 = Library.HelperUtility.FileUtility.FileMD5(item);
                if (sysInfo.Exists) fileinfo.FileSize = sysInfo.Length;
                _filesRepository.Add(fileinfo);
                if (count >= batchCount)
                {
                    count = 0;
                    _filesRepository.UnitOfWork.Commit();
                }
            }
            _filesRepository.UnitOfWork.Commit();
            var dirs = System.IO.Directory.EnumerateDirectories(dic);
            foreach (var item in dirs)
            {
                Scan(item);
            }
        }
    }
    public struct ScanderPhysicalFileOption
    {
        public string Path { get; set; }

    }
}
