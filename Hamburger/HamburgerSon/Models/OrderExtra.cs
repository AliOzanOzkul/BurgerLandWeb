namespace HamburgerWeb.Models
{
    public class OrderExtra
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public ExtraMaterial ExtraMaterial { get; set; }
    }
}
