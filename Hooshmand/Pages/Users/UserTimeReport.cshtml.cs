using ClosedXML.Excel;
using Hooshmand.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Hooshmand.Pages.Users
{
    public class UserTimeReport : PageModel
    {
        private readonly Hooshmand.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserTimeReport(Hooshmand.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Inputs Input { get; set; }

        public string TotalTime { get; set; }

        public List<UserTime> UserTimes { get; set; } = new List<UserTime>();

        public class Inputs
        {
            public ApplicationUser user { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; } = DateTime.Now;
        }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            UserTimes = await _context.UserTimes.Where(x => x.User == user).OrderByDescending(x => x.EnterTime).ToListAsync();
        }

        public async Task OnPostGetReport()
        {
            Input.ToDate = Input.ToDate.AddHours(24);

            UserTimes = await _context.UserTimes.Where(x => x.UserId == Input.user.Id && x.EnterTime >= Input.FromDate && (x.ExitTime <= Input.ToDate || x.ExitTime == null)).OrderByDescending(x => x.EnterTime).ToListAsync();

            double totalMinutes = 0;

            foreach (var item in UserTimes)
            {
                if (item.ExitTime != null)
                {
                    var a = (DateTime)item.ExitTime;
                    var b = (DateTime)item.EnterTime;
                    totalMinutes += a.Subtract(b).TotalMinutes;
                }
            }
            var time = TimeSpan.FromMinutes(totalMinutes);
            TotalTime = string.Format("{0:00}:{1:00}", (int)time.TotalHours, time.Minutes) + " ساعت";

        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            if (id == null || _context.UserTimes == null)
            {
                return NotFound();
            }

            var userTime = await _context.UserTimes.FindAsync(id);
            if (userTime != null)
            {
                _context.UserTimes.Remove(userTime);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostGetAllReports()
        {
            if (Input.FromDate.ToString() == "0001-01-01 00:00:00")
            {
                return BadRequest("مقدار های ورودی زمان را کامل کنید");
            }

            Input.ToDate = Input.ToDate.AddHours(24);
            var usersTime = await _context.UserTimes.Where(x => x.EnterTime >= Input.FromDate && x.ExitTime <= Input.ToDate).ToListAsync();
            if (usersTime != null)
            {
                List<UserTimeViewModel> viewModel = new List<UserTimeViewModel>();

                foreach (var item in usersTime.Select(x => x.UserId).Distinct())
                {
                    var userTime = usersTime.Where(x => x.UserId == item).ToList();

                    double totalMinutes = 0;

                    foreach (var utime in userTime)
                    {
                        if (utime.ExitTime != null)
                        {
                            var a = (DateTime)utime.ExitTime;
                            var b = (DateTime)utime.EnterTime;
                            totalMinutes += a.Subtract(b).TotalMinutes;
                        }
                    }

                    var time = TimeSpan.FromMinutes(totalMinutes);
                    TotalTime = string.Format("{0:00}:{1:00}", (int)time.TotalHours, time.Minutes);
                    var vacation = userTime.Select(x => x.Vacation).Where(x => x == true).Count();

                    var user = await _userManager.FindByIdAsync(item);
                    UserTimeViewModel model = new UserTimeViewModel()
                    {
                        FullName = user.FullName,
                        TotalTime = TotalTime,
                        VacationCount = vacation,
                    };
                    viewModel.Add(model);

                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("گزارش ساعت کار پرسنل");
                    var currentRow = 1;
                    worksheet.Cell(currentRow, 1).Value = "نام و نام خانوادگی";
                    worksheet.Cell(currentRow, 2).Value = "مدت زمان حضور";
                    worksheet.Cell(currentRow, 3).Value = "تعداد مرخصی";
                    foreach (var timeViewModel in viewModel)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = timeViewModel.FullName;
                        worksheet.Cell(currentRow, 2).Value = timeViewModel.TotalTime;
                        worksheet.Cell(currentRow, 3).Value = timeViewModel.VacationCount;
                    }

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();

                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "گزارش ساعت کار پرسنل.xlsx");
                    }
                }
            }
            return RedirectToPage();
        }
    }
}
