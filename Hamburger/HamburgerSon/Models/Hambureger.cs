namespace HamburgerWeb.Models
{
    public class Hambureger
    {
        public Hambureger()
        {
            OrdersHamburger = new List<OrderHamburger>();
            HamburgerBasket = new List<HamburgerBasket>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? PictureUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; } = true;
        public List<OrderHamburger> OrdersHamburger { get; set; }
        public List<HamburgerBasket> HamburgerBasket { get; set; }
    }
}
