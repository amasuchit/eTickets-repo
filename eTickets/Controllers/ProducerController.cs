using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using eTickets.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProducerController : Controller
    {
        private readonly IProducerService service;

        public ProducerController(IProducerService service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {

            var allProducers = await service.GetAllAsync();
            return View(allProducers);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] ProducerViewModel producerViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(producerViewModel);
            }
            var producer= new Producer()
            {
                FullName = producerViewModel.FullName,
                ProfilePictureURL = producerViewModel.ProfilePictureURL,
                Bio = producerViewModel.Bio
            };
            await service.AddAsync(producer);
            TempData["CreateMessage"] = "Producer's Recored Created Successfully";
            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var datafromdb = await service.GetbyIdAsync(id);
            if (datafromdb == null)
            {
                return NotFound();
            }
            var producerViewModel = new ProducerViewModel()
            {
                Id = datafromdb.Id,
                ProfilePictureURL = datafromdb.ProfilePictureURL,
                FullName = datafromdb.FullName,
                Bio = datafromdb.Bio
            };
            return View(producerViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(ProducerViewModel producerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(producerViewModel);
            }

            var producer = new Producer()
            {
                Id = producerViewModel.Id,
                FullName = producerViewModel.FullName,
                Bio = producerViewModel.Bio,
                ProfilePictureURL = producerViewModel.ProfilePictureURL
            };
            await service.UpdateAsync(producer);
            TempData["EditMessage"] = "Producer's record is updated.";
            return RedirectToAction(nameof(Index));

        }




        public async Task<IActionResult> Details(int id)
        {
            var datafromdb = await service.GetbyIdAsync(id);
            if (datafromdb == null)
            {
                return NotFound();
            }
            var producerViewModel = new ProducerViewModel()
            {
                Id = datafromdb.Id,
                FullName = datafromdb.FullName,
                ProfilePictureURL = datafromdb.ProfilePictureURL,
                Bio = datafromdb.Bio
            };
            return View(producerViewModel);
        }


        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var datafromdb = await service.GetbyIdAsync(id);
            if (datafromdb == null)
            {
                return NotFound();
            }
            var producerViewModel = new ProducerViewModel()
            {
                Id = datafromdb.Id,
                ProfilePictureURL = datafromdb.ProfilePictureURL,
                FullName = datafromdb.FullName,
                Bio = datafromdb.Bio
            };
            return View(producerViewModel);
        }



        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var datafromdb = await service.GetbyIdAsync(id);
            if (datafromdb == null)
            {
                return NotFound();
            }
            await service.DeleteAsync(id);
            TempData["DeleteMessage"] = "Producer's Record Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }



    }
}
