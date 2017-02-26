using System;

namespace HomeApplication
{
    public static class FileSotrageHelper
    {
        public static Library.Storage.IFileStorage GetStorage(this Home.DomainModel.Aggregates.FileAgg.FileInfo fileinfo)
        {
            if (fileinfo == null) throw new ArgumentNullException(nameof(fileinfo));
         
            switch (fileinfo.SourceType)
            {
                case Home.DomainModel.SourceType.ServerScand:
                    return new LocalFileStorage(fileinfo.FullPath);

                case Home.DomainModel.SourceType.PC:
                  //  if (fileinfo.Extend == null) throw new NotSupportedException();
                    break;
                case Home.DomainModel.SourceType.Mobile:
                    break;
                default:
                    break;
            }
            throw new NotSupportedException();
        }
    }
}