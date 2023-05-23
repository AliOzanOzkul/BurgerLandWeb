namespace HamburgerWeb.Models
{
    public class OrderHamburger
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public Hambureger Hambureger { get; set; }
    }
}
