﻿using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
