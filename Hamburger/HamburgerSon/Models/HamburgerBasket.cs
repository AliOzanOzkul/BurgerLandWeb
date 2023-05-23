namespace HamburgerWeb.Models
{
    public class HamburgerBasket
    {
        public int Id { get; set; }
        public Basket Basket { get; set; }
        public Hambureger Hambureger { get; set; }
    }
}
