namespace RVPark_Team2.Models
{
    public class Site
    {
        public int Id { get; set; }

        public string SiteNumber { get; set; }

        public int SiteTypeId { get; set; }

        public SiteType? SiteType { get; set; }

        public List<SitePhoto> Photos { get; set; } = new List<SitePhoto>();


    }
}
