using System.ComponentModel.DataAnnotations;

namespace Hooshmand.Models;

public class PhoneBooks : GlobalProperties
{
    [Display(Name = "نام")]
    public string FullName { get; set; }
    [Display(Name = "داخلی")]
    public int? InternalNumber { get; set; }
    [Display(Name = "شرکت")]
    public string? Company { get; set; }
    [Display(Name = "سمت شغلی")]
    public string? JobPosition { get; set; }
    [Display(Name = "واحد شغلی")]
    public string? UnitPosition { get; set; }
    [Display(Name = "توضیحات")]
    public string? Decription { get; set; }

    // Relations
    public List<Phones> Phones { get; set; } = new List<Phones>();
}

public class Phones : GlobalProperties
{
    [Phone]
    [Display(Name = "شماره تماس")]
    public string? PhoneNumber { get; set; }
    [Phone]
    [Display(Name = "فکس")]
    public string? Fax { get; set; }
    [EmailAddress]
    [Display(Name = "ایمیل")]
    public string? Email { get; set; }

    // Relations
    public int PhoneBookId { get; set; }
    public PhoneBooks? PhoneBook { get; set; }
}