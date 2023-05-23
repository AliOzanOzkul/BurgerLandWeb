namespace HamburgerWeb.Models
{
    public class ExtraMaterial
    {
        public ExtraMaterial()
        {
            ExtraOrders = new List<OrderExtra>();
            ExtraBasket = new List<ExtraBasket>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? PictureUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; } = true;
        public List<OrderExtra> ExtraOrders { get; set; }
        public List<ExtraBasket> ExtraBasket { get; set; }
    }
}
