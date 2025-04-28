using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class ActorController : Controller
    {
       
        private readonly IActorService service;

        public ActorController(IActorService service)
        {
           
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            var allActors = await service.GetAll();
            return View(allActors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                service.Add(actor);
                return RedirectToAction(nameof(Index));
            }
            
            return View(actor);
        }



    }
}
