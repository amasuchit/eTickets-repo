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
            var fordropdown= await service.DropDownForMovies();
            return View(fordropdown);
        }
            

        [HttpPost]
        public async Task<IActionResult> Create(MovieViewModel movieViewModel)
        {

            if(!ModelState.IsValid)
            {
                var fordropdown = await service.DropDownForMovies();
                movieViewModel.Cinemas = fordropdown.Cinemas;
                movieViewModel.Producers = fordropdown.Producers;
                movieViewModel.Actors = fordropdown.Actors;
                return View(movieViewModel);
            }
            await service.AddMoviewithActor(movieViewModel);
            TempData["CreateMessage"] = "Movie's Record Created Successfully";
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Details(int id)
        {
            var result = await service.GetMovieByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
           
            return View(result);
        }


    }
}
