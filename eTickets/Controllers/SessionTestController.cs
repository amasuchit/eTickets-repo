using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class SessionTestController : Controller
    {
        public IActionResult Index()
        {
            int count = HttpContext.Session.GetInt32("ClickCount") ?? 0;
            count++;
            HttpContext.Session.SetInt32("ClickCount", count);
            ViewBag.Count = count;
            return View();
        }
    }
}
