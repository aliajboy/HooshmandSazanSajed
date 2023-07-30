using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hooshmand.Models;
using Microsoft.AspNetCore.Identity;

namespace Hooshmand.Pages.Tasks.DailyTasks
{
    public class DetailsModel : PageModel
    {
        private readonly Hooshmand.Data.ApplicationDbContext _context;

        public DetailsModel(Hooshmand.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public DailyTask DailyTask { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.DailyTasks == null)
            {
                return NotFound();
            }

            var dailytask = await _context.DailyTasks.Include(x => x.ApplicationUser).FirstOrDefaultAsync(m => m.Id == id);
            if (dailytask == null)
            {
                return NotFound();
            }
            else
            {
                DailyTask = dailytask;
            }
            return Page();
        }
    }
}
