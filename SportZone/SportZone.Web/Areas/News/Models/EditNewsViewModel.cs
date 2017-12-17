namespace SportZone.Web.Areas.News.Models
{
    public class EditNewsViewModel : CreateNewsViewModel
    {
        public int Id { get; set; }

        public byte[] ImageByte { get; set; }
    }
}
