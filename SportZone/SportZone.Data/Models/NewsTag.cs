namespace SportZone.Data.Models
{
    public class NewsTag
    {
        public int NewsId { get; set; }

        public News News { get; set; }

        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}