using DemoMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoMVC.Controllers
{
    public class ViduController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(DaiLy vd)
        {
            string strOutput = vd.MaDaiLy + "-" + vd.TenDaiLy + "-" + vd.DiaChi + "-" + vd.NguoiDaiDien + "-" + vd.DienThoai + "-" + vd.MaHTPP;
            ViewBag.Data = strOutput;
            return View();
        }
    }
}