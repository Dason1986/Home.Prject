using System.Runtime.Serialization;

namespace HomeApplication.Dtos
{

    public class AlbumDto: Dto
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
}