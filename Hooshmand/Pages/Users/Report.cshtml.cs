using Hooshmand.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;

namespace Hooshmand.Pages.Users
{
    public class ReportModel : PageModel
    {
        private readonly Hooshmand.Data.ApplicationDbContext _context;

        public ReportModel(Hooshmand.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Inputs Input { get; set; }

        public List<UserTime> UserTimes { get; set; } = new List<UserTime>();

        public class Inputs
        {
            public ApplicationUser user { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; } = DateTime.Now;
        }

        public async Task OnGetAsync()
        {
        }

        public async Task OnPostGetReport()
        {
            UserTimes = await _context.UserTimes.Where(x => x.UserId == Input.user.Id && x.EnterTime >= Input.FromDate && x.ExitTime <= Input.ToDate).ToListAsync();
        }
    }
}
