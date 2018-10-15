using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portal.Models;
using Portal.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Portal.Controllers
{
    [Route("api/[controller]")]
    public class FileController : Controller
    {
		private frontendContext context;
		public FileController(){
			context = new frontendContext();
		}

        [HttpGet("{contentid}")]		
        public IActionResult GetFileByContent(int contentid)
        {
			dynamic result;
			try{
				result = context.Files.Where(a=>a.ContentId==contentid).Select(a=>new {
						id = a.Id,
						contentid = a.ContentId,
						filename = a.Filename,
						description = a.Description,
						order = a.Order
					});
			}catch(Exception ex){
				return BadRequest(ex.Message);
			}
			return Ok(result);
        }
		
		// GET: api/Files
		[HttpGet]
		[Authorize]
        public IActionResult GetFiles()
        {
			var role = User.Claims.FirstOrDefault(x => x.Type.Equals("Role")).Value;
			if(role=="Admin"){
				var result = context.Files.Select(a=>new {
					id = a.Id,
					contentId = a.ContentId,
					filename = a.Filename,
					description = a.Description,
					order = a.Order
				});
				return Ok(result);
			}
			return Unauthorized();
        }
		
		// PUT: api/Files/5
		[HttpPut("{id}")]
		[Authorize]
        public async Task<IActionResult> PutFiles([FromRoute] int id, [FromBody] Files files)
        {
			var role = User.Claims.FirstOrDefault(x => x.Type.Equals("Role")).Value;
			var idUser = User.Claims.FirstOrDefault(x => x.Type.Equals("Id")).Value;
			if(role=="Admin"){
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				if (id != files.Id)
				{
					return BadRequest();
				}

				files.CreatedBy = Convert.ToInt32(idUser);
				files.CreatedDate = DateTime.Now;
				context.Files.Update(files);

				try
				{
					await context.SaveChangesAsync();
				}
				catch (Exception)
				{
					return BadRequest();
				}

				return Ok(files);
			}
			return Unauthorized();
        }
		
        // POST: api/Files
		[HttpPost]
		[Authorize]
        public async Task<IActionResult> PostFiles(FilesUpload fileUpload)
        {
			var role = User.Claims.FirstOrDefault(x => x.Type.Equals("Role")).Value;
			var idUser = User.Claims.FirstOrDefault(x => x.Type.Equals("Id")).Value;
			if(role=="Admin"){
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				fileUpload.files.CreatedBy = Convert.ToInt32(idUser);
				fileUpload.files.CreatedDate = DateTime.Now;
				context.Files.Add(fileUpload.files);
				await context.SaveChangesAsync();

				return Ok(fileUpload.files);
			}
			return Unauthorized();
        }

        // DELETE: api/Files/5
		[HttpDelete("{id}")]
		[Authorize]
        public async Task<IActionResult> DeleteFiles([FromRoute] int id)
        {
			var role = User.Claims.FirstOrDefault(x => x.Type.Equals("Role")).Value;
			if(role=="Admin"){
				var files = context.Files.Where(a=>a.Id==id);
				if (files == null)
				{
					return NotFound();
				}
				try
				{
					foreach (var fl in files)
					{
						context.Files.Remove(fl);
					}
					await context.SaveChangesAsync();
				}
				catch (Exception ex) {
					Console.WriteLine(ex.Message);
					return BadRequest(ex.Message);
				}

				return Ok(files);
			}
			return Unauthorized();
        }
    }
}
