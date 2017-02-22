using Home.DomainModel.Aggregates.GalleryAgg;
using Library.ComponentModel.Model;
using Library.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates.FileAgg
{

    public class FileInfoExtend : CreateEntity
    {
        public FileInfoExtend()
        {

        }
        public FileInfoExtend(ICreatedInfo createinfo) : base(createinfo)
        {

        }

        public string Comments { get; set; }
        public Guid FileID { get; set; }
    


    }
  
    
}