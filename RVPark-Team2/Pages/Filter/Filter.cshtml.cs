using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RVPark_Team2.Data;
using RVPark_Team2.Models;

namespace RVPark_Team2.Pages.Filter
{
    public class FilterModel : PageModel
    {
        public void OnGet()
        {
            SiteTypes = _context.SiteTypes.ToList();

            if (StartDate == default)
            {
                StartDate = DateOnly.FromDateTime(DateTime.Today);
            }
                

            if (EndDate == default)
            {
                EndDate = StartDate.AddDays(1);
            }
                

            var dbSites = _context.Sites.AsQueryable();

            if (SelectedSiteTypeID.HasValue)
            {
                dbSites = dbSites.Where(s => s.SiteTypeId == SelectedSiteTypeID.Value);
            }

            dbSites = dbSites.Where(s =>
                !_context.Reservations.Any(r =>
                    r.SiteId == s.Id &&
                    !r.IsCancelled &&
                    DateOnly.FromDateTime(r.StartDate) < EndDate &&
                    DateOnly.FromDateTime(r.EndDate) > StartDate));

            sites = dbSites.ToList();
        }

        private readonly ApplicationDbContext _context;

        public FilterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        [Required]
        public DateOnly StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        [Required]
        public DateOnly EndDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SelectedSiteTypeID { get; set; } = null;

        public IList<Site> sites { get; set; } = new List<Site>();

        public IList<SiteType> SiteTypes { get; set; } = new List<SiteType>();

        public SelectList SiteTypesSelectList => new SelectList(SiteTypes, "Id", "Name", SelectedSiteTypeID);

        

        
    }
}
