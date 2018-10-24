using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portal.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Portal.Controllers
{
    [Route("api/[controller]")]
    public class ContentController : Controller
    {
		private frontendContext context;
		public ContentController(){
			context = new frontendContext();
		}

        [HttpGet("{url}/{type}")]		
        public IActionResult GetContentByType(string url, string type)
        {
			dynamic result;
			try{
				if(type == "full"||type == "single"||type == "feature"){
					result = context.Content.Where(a=>a.Url==url && a.IsActive==true).Select(a=>new {
						id = a.Id,
						menuid = a.MenuId,
						url = a.Url,
						title = a.Title,
						content = a.Content1,
						isactive = a.IsActive
					}).FirstOrDefault();
				}else if(type == "multiplefoto"||type == "multiplefile"||type == "multiplefilesmall"||type == "multipletext"){
					result = context.Content.Where(a=>a.Url==url && a.IsActive==true).Select(a=>new {
						id = a.Id,
						menuid = a.MenuId,
						url = a.Url,
						title = a.Title,
						content = a.Content1,
						isactive = a.IsActive
					});
				}else{
					result="";
				}
			}catch(Exception ex){
				return BadRequest(ex.Message);
			}
			return Ok(result);
        }
		
		// GET: api/Content
		[HttpGet]
		[Authorize]
        public IActionResult GetContent()
        {
			var role = User.Claims.FirstOrDefault(x => x.Type.Equals("Role")).Value;
			if(role=="Admin"){
				var result = context.Content.Select(a=>new {
					id = a.Id,
					menuId = a.MenuId,
					url = a.Url,
					title = a.Title,
					content = a.Content1,
					isActive = a.IsActive
				});
				return Ok(result);
			}
			return Unauthorized();
        }
		
		// PUT: api/Content/5
		[HttpPut("{id}")]
		[Authorize]
        public async Task<IActionResult> PutContent([FromRoute] int id, [FromBody] Content content)
        {
			var role = User.Claims.FirstOrDefault(x => x.Type.Equals("Role")).Value;
			var idUser = User.Claims.FirstOrDefault(x => x.Type.Equals("Id")).Value;
			if(role=="Admin"){
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				if (id != content.Id)
				{
					return BadRequest();
				}

				content.CreatedBy = Convert.ToInt32(idUser);
				content.CreatedDate = DateTime.Now;
				context.Content.Update(content);

				try
				{
					await context.SaveChangesAsync();
				}
				catch (Exception)
				{
					return BadRequest();
				}

				return Ok(content);
			}
			return Unauthorized();
        }
		
        // POST: api/Content
		[HttpPost]
		[Authorize]
        public async Task<IActionResult> PostContent([FromBody] Content content)
        {
			var role = User.Claims.FirstOrDefault(x => x.Type.Equals("Role")).Value;
			var idUser = User.Claims.FirstOrDefault(x => x.Type.Equals("Id")).Value;
			if(role=="Admin"){
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				content.CreatedBy = Convert.ToInt32(idUser);
				content.CreatedDate = DateTime.Now;
				context.Content.Add(content);
				await context.SaveChangesAsync();

				return Ok(content);
			}
			return Unauthorized();
        }

        // DELETE: api/Content/5
		[HttpDelete("{id}")]
		[Authorize]
        public async Task<IActionResult> DeleteContent([FromRoute] int id)
        {
			var role = User.Claims.FirstOrDefault(x => x.Type.Equals("Role")).Value;
			if(role=="Admin"){
				var content = context.Content.Where(a=>a.Id==id);
				if (content == null)
				{
					return NotFound();
				}
				try
				{
					foreach (var con in content)
					{
						context.Content.Remove(con);
					}
					await context.SaveChangesAsync();
				}
				catch (Exception ex) {
					Console.WriteLine(ex.Message);
					return BadRequest(ex.Message);
				}

				return Ok(content);
			}
			return Unauthorized();
        }
    }
}
