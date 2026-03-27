using System.ComponentModel.DataAnnotations;

namespace RVPark_Team2.Pages.Admin.Employees
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = "";

        [Required]
        public string LastName { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        public string AccessLevel { get; set; } = "";
        public bool IsLocked { get; set; }

        // Optional password
        public string? Password { get; set; }
    }
}
