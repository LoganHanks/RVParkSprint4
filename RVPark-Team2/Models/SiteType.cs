namespace RVPark_Team2.Models
{
    public class SiteType
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<SiteTypePrice> Prices { get; set; } = new List<SiteTypePrice>();

        public List<Site> Sites { get; set; } = new List<Site>();
    }
}
