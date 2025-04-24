using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class ProducerController : Controller
    {
        private readonly AppDbContext context;

        public ProducerController(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {

            var allProducers = await context.Producers.ToListAsync();
            return View(allProducers);
        }
    }
}
