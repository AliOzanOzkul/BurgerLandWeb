namespace HamburgerWeb.Models
{
    public class Order
    {
        public Order()
        {
            OrderHamburegers = new List<OrderHamburger>();
            OrderEkstras = new List<OrderExtra>();
        }
        public int Id { get; set; }
        public AppUser User { get; set; }
        public List<OrderHamburger> OrderHamburegers { get; set; }
        public List<OrderExtra> OrderEkstras { get; set; }

    }
}
