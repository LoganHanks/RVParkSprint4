using System.ComponentModel.DataAnnotations;

namespace RVPark_Team2.Pages.Admin.Employees
{
    public class CreateEmployeeClass
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string AccessLevel { get; set; } = "Staff";
        public bool IsLocked { get; set; } = false;
    }
}
