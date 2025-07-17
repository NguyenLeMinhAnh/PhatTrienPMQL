using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class HeThongPhanPhoi
    {
        [Key]
        [Display(Name = "Mã hệ thống phân phối")]
        public string? MaHTPP { get; set; }
        [Display(Name = "Tên hệ thống phân phối")]
        public string? TenHTPP { get; set; }
        public ICollection<DaiLy>? DaiLy { get; set; }
    }
}
