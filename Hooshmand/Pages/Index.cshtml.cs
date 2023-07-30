using Hooshmand.Data;
using Hooshmand.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Hooshmand.Pages;
public class IndexModel : PageModel
{
    private UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    public IndexModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async void OnGetAsync()
    {

    }

    // ثبت خروج
    public async System.Threading.Tasks.Task OnPostExitSubmit()
    {
        var user = await _userManager.GetUserAsync(User);
        List<UserTime> userTimes = await _context.UserTimes.Where(x => x.UserId == user.Id).OrderBy(x => x.EnterTime).ToListAsync() ?? new List<UserTime>();

        if (userTimes.Count != 0 && userTimes.Last().ExitTime == null)
        {
            var lastTime = await _context.UserTimes.Where(x => x.UserId == user.Id && x.EnterTime != null && x.ExitTime == null).FirstAsync();

            lastTime.ExitTime = DateTime.Now;

            await _context.SaveChangesAsync();
        }
    }

    // ثبت ورود
    public async System.Threading.Tasks.Task OnPostEnterSubmit()
    {
        var user = await _userManager.GetUserAsync(User);
        List<UserTime> userTimes = await _context.UserTimes.Where(x => x.UserId == user.Id).OrderBy(x => x.EnterTime).ToListAsync() ?? new List<UserTime>();

        if (userTimes.Count == 0 || (userTimes.Count != 0 && userTimes.Last().ExitTime != null))
        {
            UserTime time = new UserTime()
            {
                UserId = user.Id,
                EnterTime = DateTime.Now
            };

            await _context.AddAsync(time);
            await _context.SaveChangesAsync();
        }

    }
}
