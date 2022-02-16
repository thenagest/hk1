using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AwaraIt.Hackathon.WebSite.Models;
using AwaraIt.Hackathon.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace AwaraIt.Hackathon.WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static string AdminId => "d41d94da6c8efb0e";
        private string UserId => User?.Claims?.FirstOrDefault()?.Value ?? AdminId;
        
        public HomeController(ILogger<HomeController> logger)
        {
            this._logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var apiEndpoint = new Uri($"{Request.Scheme}://{Request.Host.Value}");

            var client = new HttpClient();
            var response = await client.GetStringAsync(new Uri(apiEndpoint, "api/image/"));

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var images = JsonSerializer.Deserialize<ImageVM[]>(response, options);

            return View(images);
        }

        //[Authorize]
        public async Task<IActionResult> Image(Guid id)
        {
            var apiEndpoint = new Uri($"{Request.Scheme}://{Request.Host.Value}");

            var client = new HttpClient();
            var response = await client.GetStringAsync(new Uri(apiEndpoint, $"api/image/{id}?userId={UserId}"));

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var img = JsonSerializer.Deserialize<ImageVM>(response, options);

            return View(img);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SignIn()
        {
            var props = new AuthenticationProperties
            {
                RedirectUri = "/Home/SignInSuccess"
            };

            return Challenge(props);
        }

        public IActionResult SignInSuccess()
        {
            return RedirectToAction("Index");
        }


    }
}
