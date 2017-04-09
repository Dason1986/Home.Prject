namespace Home.DomainModel.Aggregates.OfficeAgg
{

    public class WordObjectElement : ObjectElement
    {
        public virtual WordInfo Owner { get; set; }
        [System.ComponentModel.Description("元素類型"),
        System.ComponentModel.DisplayName("元素類型")]
        public WordElementType ElementType { get; set; }
        [System.ComponentModel.Description("元素對象"),
        System.ComponentModel.DisplayName("元素對象")]
        public byte[] ObjectBuffer { get; set; }
        [System.ComponentModel.Description("元素內容，能轉成文本時"),
        System.ComponentModel.DisplayName("元素內容")]
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