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

namespace Hooshmand.Pages.PhoneBook
{
    public class EditModel : PageModel
    {
        private readonly Hooshmand.Data.ApplicationDbContext _context;

        public EditModel(Hooshmand.Data.ApplicationDbContext context)
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

            var phonebooks =  await _context.PhoneBooks.FirstOrDefaultAsync(m => m.Id == id);
            if (phonebooks == null)
            {
                return NotFound();
            }
            PhoneBooks = phonebooks;
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

            _context.Attach(PhoneBooks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneBooksExists(PhoneBooks.Id))
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

        private bool PhoneBooksExists(int id)
        {
          return (_context.PhoneBooks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
