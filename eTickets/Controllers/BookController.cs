using eTickets.Data.Services;
using eTickets.Models;
using eTickets.Utilities;
using eTickets.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eTickets.Controllers
{
    public class BookController : Controller
    {
        private readonly ICinemaService _cinemaService;
        private readonly IMovieService _movieService;

        public BookController(ICinemaService cinemaService, IMovieService movieService)
        {
            _cinemaService = cinemaService;
            _movieService = movieService;
        }


        public async Task<IActionResult> Index()
        {
            
            var cinemas = await _cinemaService.GetAllAsync();
            ViewBag.Cinemas = new SelectList(cinemas, "Id", "Name");


            return View(new BookingViewModel());
        }

        [HttpGet]
        public async Task<JsonResult> GetMoviesByCinema(int cinemaId)
        {
            var movies = (await _movieService.GetAllMoviesAsync())
                .Where(m => m.CinemaId == cinemaId)
                .Select(m => new { m.Id, m.Name, m.Price })
                .ToList();

            return Json(movies);
        }


        public async Task<IActionResult> AddRowPartial()
        {
            var cinemas = await _cinemaService.GetAllAsync();
            ViewBag.Cinemas = new SelectList(cinemas, "Id", "Name");
            return PartialView("_BookingRowPartial", new BookingRowViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> Book(BookingViewModel model)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            foreach (var booking in model.Bookings)
            {
                // Optional: validate movie exists
                var movie = await _movieService.GetMovieByIdAsync(booking.MovieId);
                if (movie == null) continue;

                var existingItem = cart.FirstOrDefault(item => item.MovieId == booking.MovieId);
                if (existingItem != null)
                {
                    existingItem.Quantity += 1; // or booking.Quantity if you're supporting that
                }
                else
                {
                    cart.Add(new CartItem
                    {
                        MovieId = booking.MovieId,
                        MovieName = movie.Name,
                        Price = (decimal)movie.Price,
                        Quantity = 1
                    });
                }
            }

            // Save updated cart
            HttpContext.Session.SetObjectToJson("Cart", cart);

            return RedirectToAction("Index", "Cart");
        }

    }
}
