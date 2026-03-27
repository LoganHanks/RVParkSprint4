using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RVPark_Team2.Data;

namespace RVPark_Team2.Pages.Admin.Fees
{
    public class CreateModel : AdminPageModel
    {
        private readonly RVPark_Team2.Data.ApplicationDbContext _context;

        public CreateModel(RVPark_Team2.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int reservationId)
        {
            Fee = new Fee
            {
                ReservationId = reservationId
            };

            return Page();
        }

        [BindProperty]
        public Fee Fee { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Fees.Add(Fee);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { reservationId = Fee.ReservationId });
        }
    }
}
