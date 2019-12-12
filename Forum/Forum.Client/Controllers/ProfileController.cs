using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Client.Controllers
{
    public class ProfileController : Controller
    {
        [Authorize]
        [HttpGet("myprofile")]
        public IActionResult MyProfile()
        {
            return View();
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Sections");
        }
    }
}