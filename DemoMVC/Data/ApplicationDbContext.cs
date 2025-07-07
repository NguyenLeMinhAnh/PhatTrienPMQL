using Microsoft.EntityFrameworkCore;
using DemoMVC.Models;

namespace DemoMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}
        public DbSet<Person> Persons { get; set; }
        public DbSet<DemoMVC.Models.Employee> Employees { get; set; } = default!;
        public DbSet<DemoMVC.Models.HeThongPhanPhoi> HeThongPhanPhois { get; set; } = default!;
        public DbSet<DemoMVC.Models.DaiLy> DaiLys { get; set; } = default!;
    }
}