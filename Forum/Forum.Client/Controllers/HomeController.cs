using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Forum.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Forum.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public HomeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            using (var client = _clientFactory.CreateClient())
            {
                var response = await client.GetAsync("https://localhost:44391/api/sections");

                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();

                var sections = JsonConvert.DeserializeObject<IEnumerable<Section>>(responseBody);

                return View(sections);
            }
        }

        [Authorize]
        [HttpGet("myprofile")]
        public IActionResult MyProfile()
        {
            // QUANDO PRECISAR DO USUÁRIO ESTAR AUTENTICADO É NECESSÁRIO ENVIAR O TOKEN PARA A API.
            // PARA PEGAR O TOKEN USA:
            //
            // var accessToken = await HttpContext.GetTokenAsync("access_token");
            //
            // ENVIA ELE NO HEADER "Authorization" DA REQUISIÇÃO e "Bearer {accessToken}"

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