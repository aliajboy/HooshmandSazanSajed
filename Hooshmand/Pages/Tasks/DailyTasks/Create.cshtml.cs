using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Hooshmand.Models;
using Microsoft.AspNetCore.Identity;

namespace Hooshmand.Pages.Tasks.DailyTasks
{
    public class CreateModel : PageModel
    {
        private readonly Hooshmand.Data.ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;


        public CreateModel(Hooshmand.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DailyTask DailyTask { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            DailyTask.ApplicationUser = await _userManager.GetUserAsync(User);
            if (!ModelState.IsValid || _context.DailyTasks == null || DailyTask == null)
            {
                return Page();
            }

            _context.DailyTasks.Add(DailyTask);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
