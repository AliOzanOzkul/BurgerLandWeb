using HamburgerWeb.DTO_s;
using HamburgerWeb.Models;
using HamburgerWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HamburgerWeb.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public class BasketController : Controller
    {
        private readonly HamburgerContext _db;
        private readonly UserManager<AppUser> _manager;

        public BasketController(HamburgerContext db, UserManager<AppUser> manager)
        {
            _db = db;
            _manager = manager;
        }

        public IActionResult HamburgerDetails(int id)
        {


            ViewData["id"] = new SelectList(_db.ExtraMaterials.ToList(), "Id", "Name", "Price");

            var selectedHamburger = _db.Hamburegers.Find(id);
            HamburgerExtraMaterialsDTO extraProducts = new()
            {
                Name = selectedHamburger.Name,
                Description = selectedHamburger.Description,
                Price = selectedHamburger.Price,
                Quantity = selectedHamburger.Quantity,
                HamburgerId = selectedHamburger.Id,
                PictureUrl = selectedHamburger.PictureUrl


            };


            return View(extraProducts);
        }
        [HttpPost]
        public async Task<IActionResult> AddToBasket(HamburgerExtraMaterialsDTO extraMaterialsDTO)
        {
            var extra1 = _db.ExtraMaterials.Find(extraMaterialsDTO.Product1);
            var extra2 = _db.ExtraMaterials.Find(extraMaterialsDTO.Product2);
            var extra3 = _db.ExtraMaterials.Find(extraMaterialsDTO.Product3);
            var extra4 = _db.ExtraMaterials.Find(extraMaterialsDTO.Product4);
            var extra5 = _db.ExtraMaterials.Find(extraMaterialsDTO.Product5);
            List<ExtraMaterial> extraMalzemeList = new()
            {
                extra1,extra2, extra3, extra4, extra5
            };

            var cookie = HttpContext.Request.Cookies["Email"];
            var user = await _manager.FindByEmailAsync(cookie);
            _db.Entry(user).Reference(u => u.Basket).Load();
            var selectHam = _db.Hamburegers.FirstOrDefault(x => x.Name == extraMaterialsDTO.Name);
            if (selectHam != null)
            {
                if (user.Basket == null)
                {
                    user.Basket = new Basket();
                    for (int i = 0; i < extraMaterialsDTO.Quantity; i++)
                    {

                        user.Basket.HamburgerBasket.Add(new HamburgerBasket() { Hambureger = selectHam });
                    }
                    foreach (var ex in extraMalzemeList)
                    {
                        if (ex != null && ex.Id != 1)
                        {

                            user.Basket.ExtraBasket.Add(new ExtraBasket() { ExtraMaterial = ex });
                        }
                    }
                }

                else
                {
                    for (int i = 0; i < extraMaterialsDTO.Quantity; i++)
                    {

                        user.Basket.HamburgerBasket.Add(new HamburgerBasket() { Hambureger = selectHam });
                    }
                    foreach (var ex in extraMalzemeList)
                    {
                        if (ex != null && ex.Id != 1)
                        {

                            user.Basket.ExtraBasket.Add(new ExtraBasket() { ExtraMaterial = ex });
                        }
                    }
                }
            }

            _db.SaveChanges();



            return RedirectToAction("Index", "Home");
        }
        public IActionResult ExtraMaterialDetails(int id)
        {
            var selectedExtraMaterial = _db.ExtraMaterials.Find(id);
            return View(selectedExtraMaterial);
        }

        public async Task<IActionResult> AddToBasketExtra(ExtraMaterial extraMaterial)
        {

            var cookie = HttpContext.Request.Cookies["Email"];
            var user = await _manager.FindByEmailAsync(cookie);
            var selectedExtra = _db.ExtraMaterials.Where(x => x.Name == extraMaterial.Name).FirstOrDefault();
            _db.Entry(user).Reference(u => u.Basket).Load();
            if (user.Basket == null)
            {
                user.Basket = new Basket();
                for (int i = 0; i < extraMaterial.Quantity; i++)
                {

                    user.Basket.ExtraBasket.Add(new ExtraBasket() { ExtraMaterial = selectedExtra });
                }
            }
            else
            {
                for (int i = 0; i < extraMaterial.Quantity; i++)
                {

                    user.Basket.ExtraBasket.Add(new ExtraBasket() { ExtraMaterial = selectedExtra });
                }

            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> BasketView()
        {
            var cookie = HttpContext.Request.Cookies["Email"];
            var user = await _manager.FindByEmailAsync(cookie);
            _db.Entry(user).Reference(u => u.Basket).Load();
            var hamburgers = _db.HamburgerBaskets.Where(x => x.Basket.Id == user.Basket.Id).Select(x => x.Hambureger).ToList();
            var extras = _db.ExtraBasket.Where(x => x.Basket.Id == user.Basket.Id).Select(x => x.ExtraMaterial).ToList();

            List<HamburgerBasketVM> hamburgerBasketVMList = new List<HamburgerBasketVM>();
            foreach (var item in hamburgers)
            {




                hamburgerBasketVMList.Add(new HamburgerBasketVM()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    PhotoPath = item.PictureUrl,
                    Price = item.Price
                });

            };
            List<EkstrasBasketVM> ekstrasBasketVMList = new List<EkstrasBasketVM>();
            foreach (var item in extras)
            {




                ekstrasBasketVMList.Add(new EkstrasBasketVM()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    PhotoPath = item.PictureUrl,
                    Price = item.Price
                });

            };
            ViewBag.Id = user.Id;
            return View((hamburgerBasketVMList, ekstrasBasketVMList));

        }
        public async Task<IActionResult> HamburgerDelete(int id)
        {
            var cookie = HttpContext.Request.Cookies["Email"];
            var user = await _manager.FindByEmailAsync(cookie);
            _db.Entry(user).Reference(u => u.Basket).Load();
            var hamburgers = _db.HamburgerBaskets.Where(x => x.Basket.Id == user.Basket.Id).Select(x => x.Hambureger).ToList();
            var selectedHamburger = hamburgers.Where(x => x.Id == id).FirstOrDefault();
            var selectedHamburgerBasket = _db.HamburgerBaskets.Where(x => x.Hambureger.Id == selectedHamburger.Id).FirstOrDefault();
            _db.HamburgerBaskets.Remove(selectedHamburgerBasket);
            await _db.SaveChangesAsync();

            return RedirectToAction("BasketView", "Basket");
        }

        public async Task<IActionResult> EkstrasDelete(int id)
        {
            var cookie = HttpContext.Request.Cookies["Email"];
            var user = await _manager.FindByEmailAsync(cookie);
            _db.Entry(user).Reference(u => u.Basket).Load();
            var ekstras = _db.ExtraBasket.Where(x => x.Basket.Id == user.Basket.Id).Select(x => x.ExtraMaterial).ToList();
            var selectedMaterial = ekstras.Where(x => x.Id == id).FirstOrDefault();
            var selectedEkstrasBasket = _db.ExtraBasket.Where(x => x.ExtraMaterial.Id == selectedMaterial.Id).FirstOrDefault();
            _db.ExtraBasket.Remove(selectedEkstrasBasket);
            await _db.SaveChangesAsync();

            return RedirectToAction("BasketView", "Basket");
        }
    }
}
