using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc;
namespace DemoMVC.Controllers
{
    public class PersonController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Person ps)
        {
            string strOutput = ps.PersonId + "-" + ps.FullName + "-" + ps.Address;
            ViewBag.inforPerson = strOutput;
            return View();
        }
    }
}