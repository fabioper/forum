using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactor;

        public HomeController(IHttpClientFactory clientFactory)
        {
            _clientFactor = clientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet("myprofile")]
        public IActionResult MyProfile()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}