using HamburgerWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HamburgerWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly HamburgerContext _db;

        public AdminController(HamburgerContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var hamburgerList = _db.Hamburegers.ToList();
            var extraList = _db.ExtraMaterials.ToList();
            return View((hamburgerList, extraList));
        }
        public IActionResult CreateHamburger()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateHamburger(Hambureger hambureger, IFormFile photo)
        {
            await _db.Hamburegers.AddAsync(hambureger);
            if (photo != null && photo.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(stream);
                }
                hambureger.PictureUrl = "/img/" + fileName;

            }
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public IActionResult CreateExtraMatarial()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateExtraMatarial(ExtraMaterial extraMaterial, IFormFile photo)
        {
            await _db.ExtraMaterials.AddAsync(extraMaterial);
            if (photo != null && photo.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(stream);
                }
                extraMaterial.PictureUrl = "/img/" + fileName;

            }
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> EditExtra(int id)
        {
            var selectedExtra = await _db.ExtraMaterials.FindAsync(id);

            return View(selectedExtra);

        }

        public async Task<IActionResult> EditHamburger(int id)
        {
            var selectedHamburger = await _db.Hamburegers.FindAsync(id);

            return View(selectedHamburger);

        }
        [HttpPost]
        public async Task<IActionResult> EditExtra(ExtraMaterial extraMaterial, IFormFile photo)
        {
            if (photo != null && photo.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(stream);
                }
                extraMaterial.PictureUrl = "/img/" + fileName;

            }
            _db.ExtraMaterials.Update(extraMaterial);

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        [HttpPost]
        public async Task<IActionResult> EditHamburger(Hambureger hambureger, IFormFile photo)
        {
            if (photo != null && photo.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(stream);
                }
                hambureger.PictureUrl = "/img/" + fileName;

            }
            _db.Hamburegers.Update(hambureger);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> DeleteHamburger(int id)
        {
            var selectedHamburger = await _db.Hamburegers.FindAsync(id);
            selectedHamburger.Active = false;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteExtra(int id)
        {
            var selectedExtra = await _db.ExtraMaterials.FindAsync(id);
            selectedExtra.Active = false;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> GetActiveHamburger(int id)
        {
            var selectedHamburger = await _db.Hamburegers.FindAsync(id);
            selectedHamburger.Active = true;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> GetActiveExtra(int id)
        {
            var selectedExtra = await _db.ExtraMaterials.FindAsync(id);
            selectedExtra.Active = true;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
