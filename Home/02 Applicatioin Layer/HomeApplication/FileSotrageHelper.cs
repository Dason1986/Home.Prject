using Library.Storage.FileEngineProvider.Network;
using System;

namespace HomeApplication
{
    public static class FileSotrageHelper
    {
        public static Library.Storage.IFileStorage GetStorage(this Home.DomainModel.Aggregates.FileAgg.FileInfo fileinfo)
        {
            if (fileinfo == null) throw new ArgumentNullException(nameof(fileinfo));
            if (fileinfo.Engine == null) throw new ArgumentNullException(nameof(fileinfo.Engine));

            switch (fileinfo.SourceType)
            {
                case Home.DomainModel.SourceType.ServerScand:
                    var fullpath = System.IO.Path.Combine(fileinfo.Engine.Root, fileinfo.FullPath);
                    return new LocalFileStorage(fullpath);

                case Home.DomainModel.SourceType.PC:
                    //  if (fileinfo.Extend == null) throw new NotSupportedException();
                    break;
                case Home.DomainModel.SourceType.Mobile:
                    break;
                case Home.DomainModel.SourceType.NetWork:
                    var setting = fileinfo.Engine.Setting;
                    return new FTPFileStorage(setting.Host, setting.Uid, setting.Pwd, fileinfo.FullPath);
                  
                default:
                    break;
            }
            throw new NotSupportedException();
        }
    }
}