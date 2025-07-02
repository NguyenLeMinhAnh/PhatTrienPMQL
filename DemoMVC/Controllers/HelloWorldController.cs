using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Controllers
{
    public class HelloWorldController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.hoTen = "Nguyễn Lê Minh Anh - 2221050490";
            return View();
        }
    }
}