using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hooshmand.Data;
using Hooshmand.Models;

namespace Hooshmand.Pages.PhoneBook
{
    public class CreateModel : PageModel
    {
        private readonly Hooshmand.Data.ApplicationDbContext _context;

        public CreateModel(Hooshmand.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public PhoneBooks PhoneBooks { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync(List<Phones> phones)
        {
          if (!ModelState.IsValid || _context.PhoneBooks == null || PhoneBooks == null)
            {
                return Page();
            }

            _context.PhoneBooks.Add(PhoneBooks);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
