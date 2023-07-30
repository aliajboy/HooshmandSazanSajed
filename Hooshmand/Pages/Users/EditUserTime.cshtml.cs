using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hooshmand.Models;

namespace Hooshmand.Pages.Users
{
    public class EditUserTimeModel : PageModel
    {
        private readonly Hooshmand.Data.ApplicationDbContext _context;

        public EditUserTimeModel(Hooshmand.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserTime UserTime { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UserTimes == null)
            {
                return NotFound();
            }

            var usertime =  await _context.UserTimes.FirstOrDefaultAsync(m => m.Id == id);
            if (usertime == null)
            {
                return NotFound();
            }
            UserTime = usertime;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UserTime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTimeExists(UserTime.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./UserTimeReport");
        }

        private bool UserTimeExists(int id)
        {
          return (_context.UserTimes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
