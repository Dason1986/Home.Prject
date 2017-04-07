namespace Home.DomainModel.Aggregates.OfficeAgg
{

    public class WordObjectElement : ObjectElement
    {
        public virtual WordInfo Owner { get; set; }

        public WordElementType ElementType { get; set; }

        public byte[] ObjectBuffer { get; set; }

        public string ObjectContent { get; set; }
    }
    public enum WordElementType
    {
        None,
        Image,
        Link,
        Email,
    }
}