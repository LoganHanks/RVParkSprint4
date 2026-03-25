using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RVPark_Team2.Data;

namespace RVPark_Team2.Pages.Admin.Fees
{
    public class IndexModel : PageModel
    {
        private readonly RVPark_Team2.Data.ApplicationDbContext _context;

        public IndexModel(RVPark_Team2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Fee> Fee { get; set; } = new List<Fee>();

        public async Task OnGetAsync(int reservationId)
        {
            Fee = await _context.Fees
                .Include(f => f.Reservation)
                .Where(f => f.ReservationId == reservationId)
                .ToListAsync();
        }
    }
}
