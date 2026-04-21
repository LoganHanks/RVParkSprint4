public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public string PasswordHash { get; set; }  // store hashed password

    public string AccessLevel { get; set; }   // e.g., "Admin", "Staff", "Manager"
    public bool IsLocked { get; set; }        // true = Locked, false = Active
}