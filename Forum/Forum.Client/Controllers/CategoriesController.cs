using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Client.Models;
using Forum.Domain.Entities;
using Forum.Infra.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Client.Controllers
{
    [Route("Category")]
    public class CategoriesController : Controller
    {
        private readonly ApiService _api;

        public CategoriesController(ApiService api)
        {
            _api = api;
        }
        public async Task<IActionResult> CategoriesIndex()
        {
            var categories = await _api.GetCategories();
            return View(categories);
        }

        [HttpGet("newCategory")]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost("newCategory")]
        [Authorize]
        public async Task<IActionResult> Create(CreateCategoryViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var accessToken = await HttpContext.GetTokenAsync("access_token");
            await _api.PostCategory(vm, accessToken);

            return RedirectToAction(nameof(CategoriesIndex));
        }
    }
}