using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc;
namespace DemoMVC.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult Demo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Demo(Employee em)
        {
            string infoEmployee = em.EmployeeId + "-" + em.Age;
            ViewBag.InfoEmployee = infoEmployee;
            return View();
        }
    }
}