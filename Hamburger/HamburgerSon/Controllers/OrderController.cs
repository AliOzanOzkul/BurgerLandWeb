using HamburgerWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HamburgerWeb.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<AppUser> _manager;
        private readonly HamburgerContext _db;

        public OrderController(HamburgerContext db, UserManager<AppUser> manager)
        {
            _manager = manager;
            _db = db;
        }

        public async Task<IActionResult> OrderDetails()
        {
            //var cookie = HttpContext.Request.Cookies["Email"];
            //var user = await _manager.FindByEmailAsync(cookie);
            //_db.Entry(user).Reference(u => u.Basket).Load();
            //List<HamburgerBasket> userBasketHamburger = _db.Baskets.Include(x => x.HamburgerBasket).Select(x => x.HamburgerBasket.Where(y => y.Basket.Id == user.Basket.Id));
            //var userBasketExtra = _db.Baskets.Include(x => x.ExtraBasket).Select(x => x.ExtraBasket.Where(y => y.Basket.Id == user.Basket.Id)).ToList();
            //userBasketHamburger.Select(x => x.)
            //foreach (var item in userBasketHamburger.Select(x => x.Hambureger))
            //{

            //    _db.OrderHamburgers.Add(new() { Hambureger = })
            //}



            //Order order = new Order();
            //order.Hamburegers.Add(hamburgerList);

            //_db.Orders.Add()
            return View();
        }
    }
}
