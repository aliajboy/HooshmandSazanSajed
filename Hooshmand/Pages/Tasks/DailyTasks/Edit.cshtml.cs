using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hooshmand.Data;
using Hooshmand.Models;

namespace Hooshmand.Pages.Tasks.DailyTasks
{
    public class EditModel : PageModel
    {
        private readonly Hooshmand.Data.ApplicationDbContext _context;

        public EditModel(Hooshmand.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DailyTask DailyTask { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.DailyTasks == null)
            {
                return NotFound();
            }

            var dailytask =  await _context.DailyTasks.FirstOrDefaultAsync(m => m.Id == id);
            if (dailytask == null)
            {
                return NotFound();
            }
            DailyTask = dailytask;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DailyTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DailyTaskExists(DailyTask.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DailyTaskExists(int id)
        {
          return (_context.DailyTasks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
