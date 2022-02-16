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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AwaraIt.Hackathon.WebSite.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private string UserId => User?.Claims?.FirstOrDefault()?.Value ?? AdminId;
        
        public AdminController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            //if (UserId != AdminId)
            //    return Unauthorized();
            var client = new HttpClient();

            var apiEndpoint = new Uri($"{Request.Scheme}://{Request.Host.Value}");
            var response = await client.GetStringAsync(new Uri(apiEndpoint, "api/image/"));

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var images = JsonSerializer.Deserialize<ImageVM[]>(response, options);

            return View(images);
        }

        public async Task<IActionResult> Image(Guid id)
        {
            if (UserId != AdminId)
                return Unauthorized();
            var client = new HttpClient();
            var apiEndpoint = new Uri($"{Request.Scheme}://{Request.Host.Value}");
            var response = await client.GetStringAsync(new Uri(apiEndpoint, $"api/image/{id}"));

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var img = JsonSerializer.Deserialize<ImageVM>(response, options);

            return View(img);
        }

        private static string AdminId => "d41d94da6c8efb0e";
    }
}

