using Hooshmand.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hooshmand.Pages.Users
{
    public class AddVacationModel : PageModel
    {
        private readonly Hooshmand.Data.ApplicationDbContext _context;

        public AddVacationModel(Hooshmand.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public UserTime UserTime { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.UserTimes == null || UserTime == null)
            {
                return Page();
            }

            UserTime.ExitTime = UserTime.EnterTime;
            UserTime.Vacation = true;

            await _context.UserTimes.AddAsync(UserTime);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Users/UserTimeReport");
        }
    }
}
