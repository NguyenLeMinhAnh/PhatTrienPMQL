using DemoMVC.Models.Entities;
using DemoMVC.Models.Process;
using DemoMVC.Data;
using Bogus;


namespace DemoMVC.Models.Process
{
    public class EmployeesSeeder
    {
        private readonly ApplicationDbContext _context;
        public EmployeesSeeder(ApplicationDbContext context)
        {
            _context = context;
        }
        public void SeedEmloyees(int n)
        {
            var employeeEntity = GenerateEmployees(n);

            _context.EmployeeEntities.AddRange(employeeEntity);
            _context.SaveChanges();
        }
        private List<EmployeeEntity> GenerateEmployees(int n)
        {
            var faker = new Faker<EmployeeEntity>()
                .RuleFor(e => e.FirstName, f => f.Name.FirstName())
                .RuleFor(e => e.LastName, f => f.Name.LastName())
                .RuleFor(e => e.Address, f => f.Address.FullAddress())
                .RuleFor(e => e.DateOfBirth, f => f.Date.Past(30, DateTime.Now.AddYears(-20)))
                .RuleFor(e => e.EmailAddress, f => f.Internet.Email(f.Name.FirstName(), f.Name.LastName()))
                .RuleFor(e => e.HireDate, f => f.Date.Past(10));
            return faker.Generate(n);
        }
    }
}