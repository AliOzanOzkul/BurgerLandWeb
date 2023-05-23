namespace HamburgerWeb.DTO_s
{
    public class HamburgerExtraMaterialsDTO
    {
        public int? Product1 { get; set; }
        public int? Product2 { get; set; }
        public int? Product3 { get; set; }
        public int? Product4 { get; set; }
        public int? Product5 { get; set; }
        public int HamburgerId { get; set; }
        public string? PictureUrl { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
