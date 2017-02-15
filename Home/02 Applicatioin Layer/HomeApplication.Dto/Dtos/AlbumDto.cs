using System.Runtime.Serialization;

namespace HomeApplication.Dtos
{
    public class AlbumDto : Dto
    {
    }

    [System.Runtime.Serialization.DataContract]
    public class GalleryType
    {
        [DataMember(Order = 1)]
        public string Name
        {
            get;
            set;
        }

        [DataMember(Order = 2)]
        public int Count
        {
            get;
            set;
        }
    }

    public class FileProfile
    {
        public string Name { get; set; }
        public byte[] FileBuffer { get; set; }
        public string Extension { get; set; }
    }

    public class PhotoAttributeDto
    {
        public string AttKey { get; set; }

        public string AttValue { get; set; }
    }
}