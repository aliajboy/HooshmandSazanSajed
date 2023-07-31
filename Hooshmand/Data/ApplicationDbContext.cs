using Hooshmand.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hooshmand.Data;
public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<UserTime> UserTimes { get; set; }
    public DbSet<DailyTask> DailyTasks { get; set; }
    public DbSet<PhoneBooks> PhoneBooks { get; set; }
    public DbSet<Phones> Phones { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Customer> Customers { get; set; }

}
