using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class DaiLy
    {
        [Key]
        public string? MaDaiLy { get; set; }
        public string? TenDaiLy { get; set; }
        public string? DiaChi { get; set; }
        public string? NguoiDaiDien { get; set; }
        public string? DienThoai { get; set; }
        public string? MaHTPP { get; set; }  // Khóa ngoại (foreign key)

        // Mối liên hệ với HeThongPhanPhoi 
        public HeThongPhanPhoi? HeThongPhanPhoi { get; set; }
    }
}
