using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Forum.Client.Models;
using Forum.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Forum.Client.Controllers
{
    public class SectionsController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public SectionsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.GetAsync("https://localhost:44317/api/sections");

                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();

                var sections = JsonConvert.DeserializeObject<IEnumerable<Section>>(responseBody);

                return View(sections);
            }
        }

        [HttpGet("new")]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost("new")]
        [Authorize]
        public async Task<IActionResult> Create(CreateSectionViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            using(var client = _clientFactory.CreateClient())
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await client.PostAsync("https://localhost:44317/api/sections", 
                    new StringContent(JsonConvert.SerializeObject(vm), 
                    Encoding.UTF8, "application/json")).ConfigureAwait(true);

                response.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(Index));
            }
        }
    }
}