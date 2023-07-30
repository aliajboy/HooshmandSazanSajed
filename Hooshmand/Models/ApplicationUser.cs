using Microsoft.AspNetCore.Identity;

namespace Hooshmand.Models;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
    public string? Adress { get; set; }
    public string? Job { get; set; }

    // Relations
    public List<UserTime> Times { get; set; } = new List<UserTime>();
    public List<DailyTask> DailyTasks { get; set; } = new List<DailyTask>();
}