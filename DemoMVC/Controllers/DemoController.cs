using Microsoft.AspNetCore.Mvc;
namespace DemoMVC.Controllers
{
    public class DemoController : Controller
    {
        public ActionResult Index()  //(Phương thức Get) hiển thị form rỗng để nhập dữ liệu
        {
            return View();
        }
        [HttpPost] //chỉ định phương thức sẽ nhận dữ liệu từ View gửi lên.

        public ActionResult Index(string fullName, string Address) //(Phương thức Post) nhận, xử lý dữ liệu và hiện thị kết quả
        {
            string strOutput = "Xin chào! " + fullName + " đến từ " + Address;
            ViewBag.Message = strOutput;
            return View();
        }
    }
}