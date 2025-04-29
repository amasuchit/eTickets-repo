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
            var actorsinDb = await service.GetAllAsync();
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
            TempData["CreateMessage"] = "Actor's Record added Successfully";
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
        public async Task<IActionResult> Edit(int id)
        {
            var datafromdb = await service.GetbyIdAsync(id);
            if (datafromdb == null)
            {
                return NotFound();
            }
            var actordetail = new ActorViewModel()
            {
                Id = datafromdb.Id,
                FullName = datafromdb.FullName,
                ProfilePictureURL = datafromdb.ProfilePictureURL,
                Bio = datafromdb.Bio
            };

            return View(actordetail);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(ActorViewModel actorViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(actorViewModel);
            }
            var actor = new Actor()
            {
                Id = actorViewModel.Id,
                ProfilePictureURL=actorViewModel.ProfilePictureURL,
                FullName = actorViewModel.FullName,
                Bio = actorViewModel.Bio
            };

            await service.UpdateAsync(actor);
            TempData["EditMessage"] = "Actor's Record updated Successfully";
            return RedirectToAction(nameof(Index));
            
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
        [ValidateAntiForgeryToken]
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
            TempData["DeleteMessage"] = "Actor's Record deleted Successfully";
            return Redirect("/Actor/Index");
        }

    }
}
