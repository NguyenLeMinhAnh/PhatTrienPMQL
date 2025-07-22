using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.Entities
{
    public class EmployeeEntity
    {
        [Key]
        public int EmployeesId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Address { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
    }
}