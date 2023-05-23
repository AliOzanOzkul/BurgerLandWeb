namespace HamburgerWeb.Models
{
    public class Basket
    {
        public Basket()
        {
            Menus = new List<Menu>();
            HamburgerBasket = new List<HamburgerBasket>();
            ExtraBasket = new List<ExtraBasket>();

        }

        public string Id { get; set; }
        public AppUser User { get; set; }
        public List<Menu> Menus { get; set; }
        public List<ExtraBasket> ExtraBasket { get; set; }
        public List<HamburgerBasket> HamburgerBasket { get; set; }

    }
}
