using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Portal.Models;
using Portal.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;

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
		
		public string GetApplicationRoot()
		{
			var exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
			Regex appPathMatcher=new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
			var appRoot = appPathMatcher.Match(exePath).Value;
			return appRoot;
		}
		
		public  bool IsImage(IFormFile file)
        {
            try  
			{
				using (var bitmap = new Bitmap(file.OpenReadStream()))
				{                        
					return !bitmap.Size.IsEmpty;
				}
			}
			catch (Exception)
			{
				return false;
			}
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
        public async Task<IActionResult> PutFiles([FromRoute] int id, [FromForm] IFormFile file, [FromForm] Files files)
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
				
				try
				{
					var filename = file.FileName;
					if (filename == null)
						return Content("filename not present");
						
					var pathDir = Path.Combine(GetApplicationRoot(), "ClientApp", "src", "assets", "file");
					if(this.IsImage(file)){
						pathDir = Path.Combine(GetApplicationRoot(), "ClientApp", "src", "assets", "foto");					
					}
					var pathDirFile = Path.Combine(pathDir,filename);
					if (file.Length > 0)
					{
						using (var stream = new FileStream(pathDirFile, FileMode.Create))
						{
							await file.CopyToAsync(stream);
						}
					}
				}
				catch (Exception ex)
				{
					return BadRequest(ex);
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
        public async Task<IActionResult> PostFiles([FromForm] IFormFile file, [FromForm] Files files)
        {
			var role = User.Claims.FirstOrDefault(x => x.Type.Equals("Role")).Value;
			var idUser = User.Claims.FirstOrDefault(x => x.Type.Equals("Id")).Value;
			if(role=="Admin"){
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				
				try
				{
					var filename = file.FileName;
					if (filename == null)
						return Content("filename not present");
						
					var pathDir = Path.Combine(GetApplicationRoot(), "ClientApp", "src", "assets", "file");
					if(this.IsImage(file)){
						pathDir = Path.Combine(GetApplicationRoot(), "ClientApp", "src", "assets", "foto");					
					}
					var pathDirFile = Path.Combine(pathDir,filename);
					if (file.Length > 0)
					{
						using (var stream = new FileStream(pathDirFile, FileMode.Create))
						{
							await file.CopyToAsync(stream);
						}
					}
				}
				catch (Exception ex)
				{
					return BadRequest(ex);
				}

				files.CreatedBy = Convert.ToInt32(idUser);
				files.CreatedDate = DateTime.Now;
				context.Files.Add(files);
				await context.SaveChangesAsync();

				return Ok(files);
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
