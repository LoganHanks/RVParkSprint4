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
        public CreateEmployeeClass EmployeeVM { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var employee = new Employee
            {
                FirstName = EmployeeVM.FirstName,
                LastName = EmployeeVM.LastName,
                Email = EmployeeVM.Email,
                AccessLevel = EmployeeVM.AccessLevel,
                IsLocked = EmployeeVM.IsLocked,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(EmployeeVM.Password)
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}