using eTickets.Data;
using eTickets.Data.Enum;
using eTickets.Data.Services;
using eTickets.Models;
using eTickets.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService service;

        public MovieController(IMovieService service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            var result= await service.GetAllMoviesAsync();
            return View(result);

        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var cinemas = await service.GetAllAsync().ToListAsync();
            ViewBag.Cinemas = new SelectList(cinemas, "Id", "Name");
            var actors = await context.Actors.ToListAsync();
            ViewBag.Actors = new SelectList(actors, "Id", "FullName");
            var producers = await context.Producers.ToListAsync();
            ViewBag.Producers = new SelectList(producers, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieViewModel movieViewModel)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdowns(viewModel);
                return View(viewModel);
            }

            await _movieService.AddNewMovieAsync(viewModel);
            return RedirectToAction("Index");
        }


    }
}
