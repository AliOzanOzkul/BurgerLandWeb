using HamburgerWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace HamburgerWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly HamburgerContext _db;

        public HomeController(HamburgerContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var extraList = _db.ExtraMaterials.Where(act => act.Active == true).ToList();
            extraList.RemoveAt(0);
            var hamburgerList = _db.Hamburegers.Where(act => act.Active == true).ToList();
            var tuple = (extraList, hamburgerList);
            return View(tuple);
        }




    }
}