using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwaraIt.Hackathon.Models;
using AwaraIt.Hackathon.WebSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AwaraIt.Hackathon.WebSite.Controllers
{
    [Route("api/[controller]")]
    public class CommentController : Controller
    {
        private readonly HackathonContext Context;
        private static string AdminId => "d41d94da6c8efb0e";
        private string UserId => User?.Claims?.FirstOrDefault()?.Value ?? AdminId;

        public CommentController(HackathonContext ctx)
        {
            Context = ctx;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Guid id, [FromForm] string text)
        {
            if (!Context.Images.Any(x => x.Id == id && !x.Deleted))
                return BadRequest();

            var commentId = Guid.NewGuid();
            Context.Database.ExecuteSqlRaw(
                string.Format(InsertQuery, commentId, text, id, UserId));
            var comment = await Context.Comments.FirstOrDefaultAsync(x => x.Id == commentId);

            /*var comment = await Context.Comments.AddAsync(
                new Comment
                {
                    Message = text,
                    ImageId = id
                });*/
            //await Context.SaveChangesAsync();

            return Json(new CommentVM(comment));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {

        }

        private const string InsertQuery =
            "insert into Comments (Id, Message, ImageId, DateOfCreation, UserId, Deleted) \n" +
            "values('{0}', '{1}', '{2}', getdate(), '{3}', 0)";
    }
}

