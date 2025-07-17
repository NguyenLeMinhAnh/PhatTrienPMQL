using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models
{
    public class DaiLy
    {
        [Key]
        [Display(Name = "Mã đại lý")]
        public string? MaDaiLy { get; set; }
        [Display(Name = "Tên đại lý")]
        public string? TenDaiLy { get; set; }
        [Display(Name = "Địa chỉ")]
        public string? DiaChi { get; set; }
        [Display(Name = "Người đại diện")]
        public string? NguoiDaiDien { get; set; }
        [Display(Name = "Điện thoại")]
        public string? DienThoai { get; set; }
        [ForeignKey("MaHTPP")]
        [Display(Name = "Mã hệ thống phân phối")]
        public string? MaHTPP { get; set; }  // Khóa ngoại (foreign key)

        // Mối liên hệ với HeThongPhanPhoi 
        [ForeignKey("MaHTPP")]
        public HeThongPhanPhoi? HTPP { get; set; }
    }
}
