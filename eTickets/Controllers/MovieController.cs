using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class MovieController : Controller
    {
        private readonly AppDbContext context;

        public MovieController(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var allMovies = await context.Movies.
                Include(c=>c.Cinema).
                Include(p=>p.Producer).
                Include(a => a.Actors_Movies).
                ToListAsync();
            return View(allMovies);
        }
    }
}
