using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Forum.Client.Models;
using Forum.Domain.Entities;
using Forum.Infra.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Forum.Client.Controllers
{
    [Route("sections")]
    public class SectionsController : Controller
    {
        private readonly ApiService _api;

        public SectionsController(ApiService api)
        {
            _api = api;
        }

        [HttpGet("new")]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost("new")]
        [Authorize]
        public async Task<IActionResult> Create(CreateSectionViewModel vm, string redirectuUri)
        {
            if (!ModelState.IsValid) return View(vm);

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            await _api.PostSection(vm, accessToken);

            return RedirectToAction("Index", "Categories");
        }
    }
}