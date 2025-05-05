namespace eTickets.Models
{
    public class CartItem
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public decimal Price { get; set; }
        public string ImageURL { get; set; }
        public int Quantity { get; set; }
    }
}
