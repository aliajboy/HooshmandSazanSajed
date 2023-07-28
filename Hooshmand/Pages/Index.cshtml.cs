using Hooshmand.Data;
using Hooshmand.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Hooshmand.Pages;
public class IndexModel : PageModel
{
    private UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IndexModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _context = context;
        _signInManager = signInManager;
    }

    [TempData]
    public string StatusMessage { get; set; }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        public EditProfile EditProfile { get; set; }
        public ChangePasswordInputModel changePassword { get; set; }

    }

    public class EditProfile
    {
        [Phone]
        [Display(Name = "شماره تماس")]
        public string? PhoneNumber { get; set; }
        public string? FullName { get; set; }
        public string? Job { get; set; }
        public string? Adress { get; set; }
        [EmailAddress]
        public string Email { get; set; }

    }

    public class ChangePasswordInputModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور فعلی")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} باید حداقل {2} و حداکثر {1} کارکتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور جدید")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور جدید")]
        [Compare("NewPassword", ErrorMessage = "تکرار رمز عبور با رمز عبور جدید تفاوت دارد")]
        public string ConfirmPassword { get; set; }
    }

    public async void OnGetAsync()
    {

    }

    // ثبت خروج
    public async System.Threading.Tasks.Task OnPostExitSubmit()
    {
        var user = await _userManager.GetUserAsync(User);
        List<UserTime> userTimes = await _context.UserTimes.Where(x => x.UserId == user.Id).OrderBy(x => x.EnterTime).ToListAsync() ?? new List<UserTime>();

        if (userTimes.Count != 0 && userTimes.Last().ExitTime == null)
        {
            var lastTime = await _context.UserTimes.Where(x => x.UserId == user.Id && x.EnterTime != null && x.ExitTime == null).FirstAsync();

            lastTime.ExitTime = DateTime.Now;

            await _context.SaveChangesAsync();
        }
    }

    // ثبت ورود
    public async System.Threading.Tasks.Task OnPostEnterSubmit()
    {
        var user = await _userManager.GetUserAsync(User);
        List<UserTime> userTimes = await _context.UserTimes.Where(x => x.UserId == user.Id).OrderBy(x => x.EnterTime).ToListAsync() ?? new List<UserTime>();

        if (userTimes.Count == 0 || (userTimes.Count != 0 && userTimes.Last().ExitTime != null))
        {
            UserTime time = new UserTime()
            {
                UserId = user.Id,
                EnterTime = DateTime.Now
            };

            await _context.AddAsync(time);
            await _context.SaveChangesAsync();
        }

    }

    // ویرایش پروفایل
    public async Task<IActionResult> OnPostEditProfile()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        ModelState.Remove("Input.changePassword");
        if (!ModelState.IsValid)
        {
            StatusMessage = "خطا : مقدار های ورودی نامعتبر می‌باشند";
            return Page();
        }

        user.PhoneNumber = Input.EditProfile.PhoneNumber;
        user.Adress = Input.EditProfile.Adress;
        user.Email = Input.EditProfile.Email;
        user.FullName = Input.EditProfile.FullName;
        user.Job = Input.EditProfile.Job;

        await _context.SaveChangesAsync();
        StatusMessage = "پروفایل شما با موفقیت بروزرسانی شد";
        return RedirectToPage();
    }

    // تغییر رمز عبور
    public async Task<IActionResult> OnPostChangePassword()
    {
        ModelState.Remove("Input.EditProfile");
        if (!ModelState.IsValid)
        {
            StatusMessage = "خطا : مقدار های ورودی نامعتبر می‌باشند";
            return Page();
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.changePassword.OldPassword, Input.changePassword.NewPassword);
        if (!changePasswordResult.Succeeded)
        {
            foreach (var error in changePasswordResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }

        await _signInManager.RefreshSignInAsync(user);
        StatusMessage = "پسور شما با موفقیت تغییر کرد";

        return RedirectToPage();
    }
}
