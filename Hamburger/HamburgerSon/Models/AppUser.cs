using Microsoft.AspNetCore.Identity;

namespace HamburgerWeb.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            Orders = new List<Order>();
        }

        public List<Order> Orders { get; set; }
        public Basket Basket { get; set; }

    }
}
