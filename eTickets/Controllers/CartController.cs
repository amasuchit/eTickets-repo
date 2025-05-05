using eTickets.Data.Services;
using eTickets.Models;
using eTickets.Utilities;
using eTickets.ViewModel;
using Microsoft.AspNetCore.Mvc;

public class CartController : Controller
{
    private readonly IMovieService _context; // Your database context

    public CartController(IMovieService service)
    {
        _context = service;
    }

    // GET: /Cart
    public IActionResult Index()
    {
        var cartItems = GetCartItems();
        var cartViewModel = new CartViewModel
        {
            CartItems = cartItems,
            CartTotal = cartItems.Sum(item => item.Price * item.Quantity)
        };

        return View(cartViewModel);
    }

    // POST: /Cart/AddToCart
    [HttpPost]
    public async Task<IActionResult> AddToCart(int movieId)
    {
        // Get the movie from the database
        var movie = await _context.GetMovieByIdAsync(movieId);
        if (movie == null)
        {
            return NotFound();
        }

        // Get cart from session or create a new one
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

        // Check if movie is already in cart
        var existingItem = cart.FirstOrDefault(item => item.MovieId == movieId);
        if (existingItem != null)
        {
            // Increment quantity if already exists
            existingItem.Quantity += 1;
        }
        else
        {
            // Add new item to cart
            cart.Add(new CartItem
            {
                MovieId = movie.Id,
                MovieName = movie.Name,
                Price = (decimal)movie.Price,
                Quantity = 1
            });
        }

        // Save cart back to session
        HttpContext.Session.SetObjectToJson("Cart", cart);

        return RedirectToAction("Index");
    }

    //// POST: /Cart/UpdateQuantity
    //[HttpPost]
    //public IActionResult UpdateQuantity(int movieId, int quantity)
    //{
    //    var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");
    //    if (cart == null)
    //    {
    //        return NotFound();
    //    }

    //    var item = cart.FirstOrDefault(i => i.MovieId == movieId);
    //    if (item == null)
    //    {
    //        return NotFound();
    //    }

    //    if (quantity <= 0)
    //    {
    //        cart.Remove(item);
    //    }
    //    else
    //    {
    //        item.Quantity = quantity;
    //    }

    //    HttpContext.Session.SetObjectToJson("Cart", cart);

    //    return RedirectToAction("Index");
    //}





    [HttpPost]
    public IActionResult UpdateQuantity(int movieId, int quantity)
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
        var item = cart.FirstOrDefault(i => i.MovieId == movieId);

        if (item == null)
        {
            return Json(new { success = false, message = "Item not found in cart" });
        }

        if (quantity < 0)
        {
            quantity = 1; // Enforce minimum quantity
        }

        if (quantity == 0)
        {
            cart.Remove(item);
        }
        else
        {
            item.Quantity = quantity;
        }

        HttpContext.Session.SetObjectToJson("Cart", cart);

        // Calculate totals
        var itemTotal = item != null && quantity > 0 ? item.Price * item.Quantity : 0;
        var cartTotal = cart.Sum(i => i.Price * i.Quantity);

        return Json(new
        {
            success = true,
            itemTotal = itemTotal,
            cartTotal = cartTotal,
            currentQuantity = quantity
        });
    }








    // POST: /Cart/RemoveFromCart
    [HttpPost]
    public IActionResult RemoveFromCart(int movieId)
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");
        if (cart == null)
        {
            return NotFound();
        }

        var item = cart.FirstOrDefault(i => i.MovieId == movieId);
        if (item != null)
        {
            cart.Remove(item);
        }

        HttpContext.Session.SetObjectToJson("Cart", cart);

        return RedirectToAction("Index");
    }

    private List<CartItemViewModel> GetCartItems()
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();
        return cart.Select(item => new CartItemViewModel
        {
            MovieId = item.MovieId,
            MovieName = item.MovieName,
            Price = item.Price,
            Quantity = item.Quantity,
            TotalPrice = (decimal)(item.Price * item.Quantity)
        }).ToList();
    }
}