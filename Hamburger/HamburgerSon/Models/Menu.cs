using HamburgerWeb.Enums;

namespace HamburgerWeb.Models
{
    public class Menu
    {
        public Menu()
        {

            Orders = new List<Order>();
            Basket = new List<Basket>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public SizeEnum Size { get; set; } = SizeEnum.Small;

        public List<Order> Orders { get; set; }
        public List<Basket> Basket { get; set; }

    }
}
