using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hooshmand.Models;

public class UserTime : GlobalProperties
{
    public DateTime? EnterTime { get; set; }
    public DateTime? ExitTime { get; set; }
    public bool Vacation { get; set; } = false;

    // Relations
    [ForeignKey(nameof(UserId))]
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}
