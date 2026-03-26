namespace RVPark_Team2.Models
    //used AI to debug and make look prettier and fit the PascalCase scheme.
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AccessLevel { get; set; } // e.g., "Admin", "Staff", "Manager"
        public bool IsLocked { get; set; }      // true = Locked, false = Active
    }
}
