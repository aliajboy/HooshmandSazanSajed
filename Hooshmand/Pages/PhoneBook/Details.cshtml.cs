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
    public class DetailsModel : PageModel
    {
        private readonly Hooshmand.Data.ApplicationDbContext _context;

        public DetailsModel(Hooshmand.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
            else
            {
                PhoneBooks = phonebooks;
            }
            return Page();
        }
    }
}
