using Microsoft.EntityFrameworkCore;
using DemoMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DemoMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}
        public DbSet<Person> Persons { get; set; }
        public DbSet<DemoMVC.Models.Employee> Employee { get; set; } = default!;
        public DbSet<DemoMVC.Models.HeThongPhanPhoi> HeThongPhanPhoi { get; set; } = default!;
        public DbSet<DemoMVC.Models.DaiLy> DaiLy { get; set; } = default!;
    }
}