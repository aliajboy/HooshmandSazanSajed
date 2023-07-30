using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hooshmand.Data;
using Hooshmand.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hooshmand.Pages.Tasks.DailyTasks
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<DailyTask> DailyTask { get; set; } = default!;

        [BindProperty]
        public Inputs Input { get; set; }

        public class Inputs
        {
            public ApplicationUser user { get; set; }
            public DateTime Date { get; set; }
        }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (_context.DailyTasks != null)
            {
                var dailyTasks = await _context.DailyTasks.Where(x => x.UserId == user.Id).ToListAsync();
                foreach (var item in dailyTasks)
                {
                    item.Description = item.Description.Substring(0, 100) + " ...";
                }
                DailyTask = dailyTasks;
            }
        }

        public async Task OnPostAsync()
        {
            if (_context.DailyTasks != null)
            {
                var dailyTasks = await _context.DailyTasks.Where(x => x.UserId == Input.user.Id && x.DateTime >= Input.Date && x.DateTime <= Input.Date.AddHours(24)).ToListAsync();
                foreach (var item in dailyTasks)
                {
                    item.Description = item.Description.Substring(0, 100) + " ...";
                }
                DailyTask = dailyTasks;
            }
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            if (id == null || _context.DailyTasks == null)
            {
                return NotFound();
            }

            var dailytask = await _context.DailyTasks.FindAsync(id);

            if (dailytask != null)
            {
                _context.DailyTasks.Remove(dailytask);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./index");
        }
    }
}
