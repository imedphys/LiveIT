using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
