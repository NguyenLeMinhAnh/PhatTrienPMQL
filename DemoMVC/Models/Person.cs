using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models
{
    public class Person
    {
        [Key]
        public string? PersonId { get; set; }
        [MinLength(5, ErrorMessage = "Full name must be at least i characters long.")]
        public required string FullName { get; set; }
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public required string Email { get; set; }
        public string? Address { get; set; }
    }
}
