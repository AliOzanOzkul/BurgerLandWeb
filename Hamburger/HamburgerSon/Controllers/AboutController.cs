using Microsoft.AspNetCore.Mvc;

namespace HamburgerWeb.Controllers
{
    public class AboutController : Controller
    {
        [Route("/About")]
        public IActionResult About()
        {
            return View();
        }
    }
}
