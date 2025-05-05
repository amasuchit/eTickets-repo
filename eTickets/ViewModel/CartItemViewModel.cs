using eTickets.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace eTickets.ViewModel
{
    public class CartItemViewModel
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; internal set; }
    }
}
