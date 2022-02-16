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
    public class LikeController : Controller
    {
        private readonly HackathonContext Context;

        public LikeController(HackathonContext ctx)
        {
            Context = ctx;
        }

        private string UserId => User?.Claims?.FirstOrDefault()?.Value ?? null;

        // GET: api/values
        [HttpGet]
        public IActionResult Get(Guid imageId)
        {
            return Json(
                Context.Likes.FirstOrDefault(x =>
                    x.UserId == UserId &&
                    x.ImageId == imageId &&
                    !x.Deleted) != null);
        }


        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Guid imageId)
        {
            var like = Context.Likes.FirstOrDefault(x =>
                    x.UserId == UserId &&
                    x.ImageId == imageId &&
                    !x.Deleted);
            if(like == null)
            {
                await Context.AddAsync(new Like { ImageId = imageId, UserId = UserId });
                await Context.SaveChangesAsync();
            }
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid imageId)
        {
            var like = Context.Likes.FirstOrDefault(x =>
                    x.UserId == UserId &&
                    x.ImageId == imageId &&
                    !x.Deleted);
            if (like != null)
                like.Deleted = true;
            await Context.SaveChangesAsync();
            return Ok();
        }
    }
}

