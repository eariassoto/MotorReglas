using MotorReglas.Models;
using System.Web.Mvc;

namespace MotorReglas.Controllers
{
    public class HomeController : Controller
    {
        private readonly RuleEngineDBEntities _db = new RuleEngineDBEntities();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }
    }
}
