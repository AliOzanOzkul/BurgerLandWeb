using HamburgerWeb.Models;
using HamburgerWeb.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HamburgerWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {

            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp([FromForm] SignUpVM signUpVM, string role = "User")
        {
            var control = await _userManager.FindByEmailAsync(signUpVM.Email);



            if (control == null)
            {

                AppUser appUser = new()
                {
                    UserName = signUpVM.UserName,
                    Email = signUpVM.Email,

                };
                appUser.Basket = new Basket();
                var result = await _userManager.CreateAsync(appUser, signUpVM.Password);
                await _userManager.AddToRoleAsync(appUser, role);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        public async Task<IActionResult> SignIn()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn([FromForm] SignInVM signInVM)
        {

            var user = await _userManager.FindByEmailAsync(signInVM.Email);

            var result = await _signInManager.CheckPasswordSignInAsync(user, signInVM.Password, false);
            if (result != null)
            {
                var getRole = await _userManager.GetRolesAsync(user);
                var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, getRole[0])
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var autProperties = new AuthenticationProperties()
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2)
                };
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1),
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                };
      
                Response.Cookies.Append("Email", user.Email, cookieOptions);
                Response.Cookies.Append("Role", getRole[0], cookieOptions);

                await _signInManager.SignInWithClaimsAsync(user, autProperties, claims);
                return RedirectToAction("Index", "Home");

            }

            return View();
        }
        public async Task<IActionResult> SignOut()
        {
            var user = HttpContext.Request.Cookies["Email"];
            var selectedUser = await _userManager.FindByEmailAsync(user);
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}

