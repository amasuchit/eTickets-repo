using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using eTickets.ViewModel;
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
            var actorsinDb = await service.GetAll();
            if (actorsinDb == null)
            {
                return NotFound();
            }

            var allActors = actorsinDb.Select(a => new ActorViewModel()
            {
                Id = a.Id,
                FullName = a.FullName,
                ProfilePictureURL = a.ProfilePictureURL,
                Bio = a.Bio
            });
            return View(allActors);

        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] ActorViewModel actorViewModel)
        {
            var actor = new Actor()
            {
                FullName = actorViewModel.FullName,
                ProfilePictureURL = actorViewModel.ProfilePictureURL,
                Bio = actorViewModel.Bio
            };

            if (!ModelState.IsValid)
            {
                return View(actorViewModel);
            }
            await service.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var detailsfromDb = await service.GetbyIdAsync(id);
            if (detailsfromDb == null)
            {
                return NotFound();
            }
            var actorDetails = new ActorViewModel()
            {
                Id=detailsfromDb.Id,
                FullName = detailsfromDb.FullName,
                ProfilePictureURL = detailsfromDb.ProfilePictureURL,
                Bio = detailsfromDb.Bio
            };
            return View(actorDetails);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var detailsfromDb = await service.GetbyIdAsync(id);
            if (detailsfromDb == null)
            {
                return NotFound();
            }
            var actorDetails = new ActorViewModel()
            {
                Id= detailsfromDb.Id,
                FullName = detailsfromDb.FullName,
                ProfilePictureURL = detailsfromDb.ProfilePictureURL,
                Bio = detailsfromDb.Bio
            };
            return View(actorDetails);

        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var detailsfromDb = await service.GetbyIdAsync(id);
            if (detailsfromDb == null)
            {
                return NotFound();
            }
            await service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
