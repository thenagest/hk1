using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AwaraIt.Hackathon.Models;
using AwaraIt.Hackathon.WebSite.Models;
using System.IO;
using System.Net;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AwaraIt.Hackathon.WebSite.Controllers
{
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        private readonly HackathonContext Context;

        private string UserId => User?.Claims?.FirstOrDefault()?.Value ?? AdminId;

        public ImageController(HackathonContext ctx)
        {
            Context = ctx;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get(string userId) =>
            Json(
                Context.Images.
                Where(x => !x.Deleted).
                Include(x => x.Comments.Where(c => !c.Deleted)).
                Include(x => x.Likes.Where(l => !l.Deleted)).
                ToList().
                OrderBy(_ => Guid.NewGuid()).
                Select(x => new ImageVM(x, userId ?? UserId)));

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id, string userId) =>
            Context.Images.
            Include(x => x.Comments.Where(c => !c.Deleted)).
            Include(x => x.Likes.Where(l => !l.Deleted)).
            FirstOrDefault(x => x.Id == id) is var img ?
            Json(new ImageVM(img, userId ?? UserId)) : NotFound();

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]IFormFile file)
        {
            var userId = User?.Claims?.FirstOrDefault()?.Value ?? AdminId;

            var imageName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            // todo
            var root = Path.GetFullPath("wwwroot");
            var uploadPath = "/images/upload/";
            //var path = Path.Combine(root, uploadPath, imageName);
            var path = root + uploadPath + imageName;
            await file.CopyToAsync(new FileStream(path, FileMode.Create));
            var url = $"/images/upload/{imageName}";
            var image = Context.Add(new Image { Url = url, UserId = userId });
            await Context.SaveChangesAsync();
            return Json(new ImageVM(image.Entity, UserId));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var img = await Context.Images.FirstOrDefaultAsync(x => x.Id == id);
            if (img == null)
                return NotFound();
            img.Deleted = true;
            await Context.SaveChangesAsync();
            return Ok();
        }

        private static string AdminId => "d41d94da6c8efb0e";
    }
}

