using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Hooshmand.Data;
using Hooshmand.Models;

namespace Hooshmand.Pages.PhoneBook
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
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

            var phonebooks = await _context.PhoneBooks.Include(x => x.Phones).FirstOrDefaultAsync(m => m.Id == id);
            if (phonebooks == null)
            {
                return NotFound();
            }
            PhoneBooks = phonebooks;
            return Page();
        }


        public async Task<IActionResult> OnPostAsync(IEnumerable<Phones> phones)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var phoneList = await _context.Phones.Where(x => x.PhoneBookId == PhoneBooks.Id).ToListAsync();

            _context.Phones.RemoveRange(phoneList);

            foreach (var phone in phones)
            {
                phone.PhoneBook = PhoneBooks;
            }
            PhoneBooks.Phones.AddRange(phones);

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
