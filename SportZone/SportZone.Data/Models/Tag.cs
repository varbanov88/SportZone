using System.Collections.Generic;

namespace SportZone.Data.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public List<NewsTag> NewsTagged { get; set; } = new List<NewsTag>();
    }
}