using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hooshmand.Data;
using Hooshmand.Models;

namespace Hooshmand.Pages.PhoneBook
{
    public class DeleteModel : PageModel
    {
        private readonly Hooshmand.Data.ApplicationDbContext _context;

        public DeleteModel(Hooshmand.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public PhoneBooks PhoneBooks { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PhoneBooks == null)
            {
                return NotFound();
            }

            var phonebooks = await _context.PhoneBooks.FirstOrDefaultAsync(m => m.Id == id);

            if (phonebooks == null)
            {
                return NotFound();
            }
            else 
            {
                PhoneBooks = phonebooks;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.PhoneBooks == null)
            {
                return NotFound();
            }
            var phonebooks = await _context.PhoneBooks.FindAsync(id);

            if (phonebooks != null)
            {
                PhoneBooks = phonebooks;
                _context.PhoneBooks.Remove(PhoneBooks);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
