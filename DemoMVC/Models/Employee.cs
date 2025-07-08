using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models
{
    [Table("Emloyee")]
    public class Employee : Person
    {
        public string? EmployeeId { get; set; }
        public int Age { get; set; }
    }
}
