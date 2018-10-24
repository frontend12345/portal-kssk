using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;  
using Portal.Models;
using AspNetCore.Totp;

namespace Portal.Controllers
{
    [Route("api/[controller]")]
    public class NewsController : Controller
    {
		private frontendContext context;
        private IConfiguration _configuration { get; set; }
		
		public NewsController(frontendContext con, IConfiguration configuration){
			context = con;
			_configuration = configuration;
		}
		
		// GET: api/News
		[HttpGet]
		[Authorize]
        public IActionResult GetNews()
        {
			var role = User.Claims.FirstOrDefault(x => x.Type.Equals("Role")).Value;
			if(role=="Admin"){
				var result = context.News.Select(a=>new {
					id = a.Id,
					text = a.Text,
					url = a.Url
				});
				return Ok(result);
			}
			return Unauthorized();
        }
		
		// PUT: api/News/5
		[HttpPut("{id}")]
		[Authorize]
        public async Task<IActionResult> PutNews([FromRoute] int id, [FromBody] News news)
        {
			var role = User.Claims.FirstOrDefault(x => x.Type.Equals("Role")).Value;
			var idUser = User.Claims.FirstOrDefault(x => x.Type.Equals("Id")).Value;
			if(role=="Admin"){
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				if (id != news.Id)
				{
					return BadRequest();
				}
				
				news.CreatedBy = Convert.ToInt32(idUser);
				news.CreatedDate = DateTime.Now;
				context.News.Update(news);

				try
				{
					await context.SaveChangesAsync();
				}
				catch (Exception)
				{
					return BadRequest();
				}

				return Ok(news);
			}
			return Unauthorized();
        }
		
        // POST: api/News
		[HttpPost]
		[Authorize]
        public async Task<IActionResult> PostNews([FromBody] News news)
        {
			var role = User.Claims.FirstOrDefault(x => x.Type.Equals("Role")).Value;
			var idUser = User.Claims.FirstOrDefault(x => x.Type.Equals("Id")).Value;
			if(role=="Admin"){
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				news.CreatedBy = Convert.ToInt32(idUser);
				news.CreatedDate = DateTime.Now;
				context.News.Add(news);
				await context.SaveChangesAsync();

				return Ok(news);
			}
			return Unauthorized();
        }

        // DELETE: api/News/5
		[HttpDelete("{id}")]
		[Authorize]
        public async Task<IActionResult> DeleteNews([FromRoute] int id)
        {
			var role = User.Claims.FirstOrDefault(x => x.Type.Equals("Role")).Value;
			if(role=="Admin"){
				var news = context.News.Where(a=>a.Id==id);
				if (news == null)
				{
					return NotFound();
				}
				try
				{
					foreach (var nw in news)
					{
						context.News.Remove(nw);
					}
					await context.SaveChangesAsync();
				}
				catch (Exception ex) {
					Console.WriteLine(ex.Message);
					return BadRequest(ex.Message);
				}

				return Ok(news);
			}
			return Unauthorized();
        }
    }
}
