using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using eTickets.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class CinemaController : Controller
    {
        private readonly ICinemaService service;

        public CinemaController(ICinemaService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            var datafromDb = await service.GetAllAsync();
            if (datafromDb == null)
            {
                return NotFound();
            }
            var cinemaViewModel = datafromDb.Select(c => new CinemaViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                CinemaLogo = c.CinemaLogo,
                Description = c.Description
            });
            return View(cinemaViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CinemaViewModel cinemaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(cinemaViewModel);
            }
            var cinema = new Cinema()
            {
                Id = cinemaViewModel.Id,
                Name = cinemaViewModel.Name,
                CinemaLogo = cinemaViewModel.CinemaLogo,
                Description = cinemaViewModel.Description
            };
            await service.AddAsync(cinema);
            TempData["CreateMessage"] = "Cinema's record created Successfully";
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var datafromDb = await service.GetbyIdAsync(id);
            if (datafromDb == null)
            {
                return NotFound();
            }
            var cinameViewModel = new CinemaViewModel()
            {
                Id = datafromDb.Id,
                Name = datafromDb.Name,
                CinemaLogo = datafromDb.CinemaLogo,
                Description = datafromDb.Description
            };
            return View(cinameViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CinemaViewModel cinemaViewModel)
        {
            if (cinemaViewModel.Id == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(cinemaViewModel);
            }
            var cinema = new Cinema()
            {
                Id = cinemaViewModel.Id,
                Name = cinemaViewModel.Name,
                CinemaLogo = cinemaViewModel.CinemaLogo,
                Description = cinemaViewModel.Description
            };
            await service.UpdateAsync(cinema);
            TempData["EditMessage"] = "Cinema's record updated Successfully";
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var datafromDb = await service.GetbyIdAsync(id);
            if (datafromDb == null)
            {
                return NotFound();
            }
            var cinemaViewModel = new CinemaViewModel()
            {
                Id = datafromDb.Id,
                CinemaLogo = datafromDb.CinemaLogo,
                Name = datafromDb.Name,
                Description = datafromDb.Description
            };
            return View(cinemaViewModel);
        }



        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var datafromDb = await service.GetbyIdAsync(id);
            if (datafromDb == null)
            {
                return NotFound();
            }
            var cinemaViewModel = new CinemaViewModel()
            {
                Id = datafromDb.Id,
                CinemaLogo = datafromDb.CinemaLogo,
                Name = datafromDb.Name,
                Description = datafromDb.Description
            };
            return View(cinemaViewModel);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var datafromDb = await service.GetbyIdAsync(id);
            if (datafromDb == null)
            {
                return NotFound();
            }
            await service.DeleteAsync(id);
            TempData["DeleteMessage"] = "Cinema's record deleted Successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
