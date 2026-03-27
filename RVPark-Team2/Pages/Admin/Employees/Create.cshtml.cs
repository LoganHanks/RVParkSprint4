using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RVPark_Team2.Data;
using RVPark_Team2.Models;

namespace RVPark_Team2.Pages.Admin.Employees
{
    public class CreateModel : AdminPageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Employee Employee { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var employee = new Employee
            {
                FirstName = Employee.FirstName,
                LastName = Employee.LastName,
                Email = Employee.Email,
                AccessLevel = Employee.AccessLevel,
                IsLocked = Employee.IsLocked,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(Employee.PasswordHash)
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}