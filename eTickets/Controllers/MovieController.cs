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
            return View();
        }
            

        [HttpPost]
        public async Task<IActionResult> Create(MovieViewModel movieViewModel)
        {
           
            return RedirectToAction("Index");
        }


    }
}
