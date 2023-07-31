using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hooshmand.Data;
using Hooshmand.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hooshmand.Pages.PhoneBook
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<PhoneBooks> PhoneBooks { get; set; } = default!;

        [BindProperty]
        public string Search { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.PhoneBooks != null)
            {
                PhoneBooks = await _context.PhoneBooks.Include(w => w.Phones).ToListAsync();
            }
        }

        public async Task OnPostSearch()
        {
            if (_context.PhoneBooks != null && Search != null)
            {
                PhoneBooks = await _context.PhoneBooks.Include(w => w.Phones).Where(x => x.FullName.Contains(Search) || x.Phones.Select(x => x.PhoneNumber).Contains(Search) ||
                x.Company.Contains(Search) || x.Phones.Select(y => y.Email).Contains(Search)).ToListAsync();
            }
            else
            {
                await OnGetAsync();
            }
        }

        public async Task<IActionResult> OnPostDelete(int? id)
        {
            if (id == null || _context.PhoneBooks == null)
            {
                return NotFound();
            }
            var phonebooks = await _context.PhoneBooks.FindAsync(id);

            if (phonebooks != null)
            {
                _context.PhoneBooks.Remove(phonebooks);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
