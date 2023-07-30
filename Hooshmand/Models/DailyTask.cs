using System.ComponentModel.DataAnnotations;

namespace Hooshmand.Models;

public class DailyTask : GlobalProperties
{
    [Display(Name = "عنوان")]
    public string Title { get; set; }
    [Display(Name = "توضیحات")]
    public string Description { get; set; }
    [Display(Name = "تاریخ")]
    public DateTime DateTime { get; set; } = DateTime.Now;

    // Relations
    [Display(Name = "کاربر")]
    public string UserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }
}
