namespace RVPark_Team2.Models
{
    public class SitePhoto
    {
        public int Id { get; set; }

        public int SiteId { get; set; }

        public required string PhotoUrl { get; set; }

        public Site PSite { get; set; }


    }
}
