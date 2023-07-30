using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hooshmand.Models;

public class UserTime : GlobalProperties
{
    [Display(Name = "زمان ورود")]
    public DateTime? EnterTime { get; set; }
    [Display(Name = "زمان خروج")]
    public DateTime? ExitTime { get; set; }
    [Display(Name = "مرخصی")]
    public bool Vacation { get; set; } = false;

    // Relations
    [ForeignKey(nameof(UserId))]
    public string UserId { get; set; }
    public ApplicationUser? User { get; set; }
}

public class UserTimeViewModel
{
    public string FullName { get; set; }
    public int VacationCount { get; set; }
    public string TotalTime { get; set; }
}