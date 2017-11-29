namespace Home.DomainModel.Aggregates.GalleryAgg
{

    public class EequipmentItem
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Count { get; set; }
    }

    public class StatisticsItem
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
}