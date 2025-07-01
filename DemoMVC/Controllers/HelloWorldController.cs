using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Controllers
{
    public class HelloWorldController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public string Wellcome()
        {
            return "This is the Wellcome action method...";
        }
    }
}