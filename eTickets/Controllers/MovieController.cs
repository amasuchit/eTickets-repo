using eTickets.Data;
using eTickets.Data.Enum;
using eTickets.Data.Services;
using eTickets.Models;
using eTickets.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{

    [Authorize(Roles ="Admin")]
    public class MovieController : Controller
    {
        private readonly IMovieService service;
        private readonly ICinemaService _cinemaService;

        public MovieController(IMovieService service, ICinemaService cinemaService)
        {
            this.service = service;
            _cinemaService = cinemaService;
        }

        [Authorize(Roles ="User")]
        public async Task<IActionResult> Index(int? cinemaId, int? movieId)
        {
            var movies = await service.GetFilteredMoviesAsync(cinemaId, movieId);

            var cinemas = await _cinemaService.GetAllAsync();
            var allMovies= await service.GetAllMoviesAsync();
            var filterVM = new MovieFilterViewModel
            {
                SelectedCinemaId = cinemaId,
                SelectedMovieId = movieId,
                Cinemas = cinemas.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }),
                Movies = allMovies.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }),
                FilteredMovies = movies
            };
            ViewBag.Cinemas = new SelectList(cinemas, "Id", "Name");


            return View(filterVM);
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var datafromdb = await service.GetMovieByIdAsync(id);
            if (datafromdb == null)
            {
                return NotFound();
            }
            var fordropdown = await service.DropDownForMovies();
            var movieViewModel = new MovieViewModel()
            {
                Id = datafromdb.Id,
                Name = datafromdb.Name,
                Description = datafromdb.Description,
                Price = datafromdb.Price,
                StartDate = datafromdb.StartDate,
                EndDate = datafromdb.EndDate,
                ImageURL = datafromdb.ImageURL,
                MovieCategory = datafromdb.MovieCategory,
                CinemaId = datafromdb.CinemaId,
                ProducerId = datafromdb.ProducerId,
                ActorId = datafromdb.Actors_Movies.Select(a => a.ActorId).ToList(),
                Cinemas = fordropdown.Cinemas,
                Producers = fordropdown.Producers,
                Actors = fordropdown.Actors
            };
            return View(movieViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(MovieViewModel movieViewModel)
        {
            
            if (!ModelState.IsValid)
            {
                var fordropdown = await service.DropDownForMovies();
                movieViewModel.Cinemas = fordropdown.Cinemas;
                movieViewModel.Producers = fordropdown.Producers;
                movieViewModel.Actors = fordropdown.Actors;
                return View(movieViewModel);
            }
            await service.UpdateMovieAsync(movieViewModel);
            TempData["EditMessage"] = "Movie's Record Updated Successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var result = await service.GetMovieByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            var fordropdown = await service.DropDownForMovies();
            var viewModel = new MovieViewModel()
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                Price = result.Price,
                StartDate = result.StartDate,
                EndDate = result.EndDate,
                ImageURL = result.ImageURL,
                MovieCategory = result.MovieCategory,
                CinemaId = result.CinemaId,
                ProducerId = result.ProducerId,
                Actors = result.Actors_Movies.Select(a => new SelectListItem
                {
                    Text = a.Actor.FullName,
                    Value = a.ActorId.ToString()
                }).ToList(),
                Cinemas = fordropdown.Cinemas,
                Producers = fordropdown.Producers,
                
            };
            
            return View(viewModel);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await service.GetMovieByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            await service.DeleteAsync(id);
            TempData["DeleteMessage"] = "Movie's Record Deleted Successfully";
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<JsonResult> GetMoviesByCinema(int cinemaId)
        {
            var movies = (await service.GetAllMoviesAsync())
                .Where(m => m.CinemaId == cinemaId)
                .Select(m => new { m.Id, m.Name })
                .ToList();

            return Json(movies);
        }

    }
}
