using eTickets.Data;
using eTickets.Data.Services;
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
    }
}
