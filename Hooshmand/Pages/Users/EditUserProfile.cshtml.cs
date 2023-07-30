using Hooshmand.Data;
using Hooshmand.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Hooshmand.Pages.Users
{
    public class EditUserProfileModel : PageModel
    {

        private UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public EditUserProfileModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public Input Inputs { get; set; } = default!;

        public class Input
        {
            [Phone]
            [Display(Name = "شماره تماس")]
            public string? PhoneNumber { get; set; }
            [Display(Name = "نام و نام خانوادگی")]
            public string? FullName { get; set; }
            [Display(Name = "شغل")]
            public string? Job { get; set; }
            [Display(Name = "آدرس")]
            public string? Adress { get; set; }
            [EmailAddress]
            [Display(Name = "ایمیل")]
            public string Email { get; set; }

        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"کاربری با ID : '{_userManager.GetUserId(User)} پیدا نشد'.");
            }

            var input = new Input()
            {

                FullName = user.FullName,
                Adress = user.Adress,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Job = user.Job
            };

            Inputs = input;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"کاربری با ID : '{_userManager.GetUserId(User)} پیدا نشد'.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            user.PhoneNumber = Inputs.PhoneNumber;
            user.Adress = Inputs.Adress;
            user.Email = Inputs.Email;
            user.FullName = Inputs.FullName;
            user.Job = Inputs.Job;

            await _context.SaveChangesAsync();
            return RedirectToPage("/Users/Index");
        }
    }
}
