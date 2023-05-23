namespace HamburgerWeb.Models
{
    public class ExtraBasket
    {
        public int Id { get; set; }
        public Basket Basket { get; set; }
        public ExtraMaterial ExtraMaterial { get; set; }
    }
}
