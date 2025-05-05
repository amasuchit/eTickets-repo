namespace eTickets.ViewModel
{
    public class CartViewModel
    {
        public List<CartItemViewModel> CartItems { get; set; } = new List<CartItemViewModel>();
        public decimal CartTotal { get; set; }
    }
}
