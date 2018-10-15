using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Portal.Models;
using System.Security.Claims;
using Microsoft.Extensions.FileProviders;
using System.IO;
using AspNetCore.Totp.Interface;
using AspNetCore.Totp;
using System.Text.RegularExpressions;

namespace Portal.Controllers
{
    [Route("api/[controller]")]
    public class SecureFileController : Controller
    {
		private frontendContext context;
        private readonly ITotpValidator totpValidator;
        private readonly ITotpGenerator totpGenerator;
		
		public SecureFileController(frontendContext con){
			context = con;
            totpGenerator = new TotpGenerator();
            totpValidator = new TotpValidator(this.totpGenerator);
		}
		
		public string GetApplicationRoot()
		{
			var exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
			Regex appPathMatcher=new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
			var appRoot = appPathMatcher.Match(exePath).Value;
			return appRoot;
		}
		
		// GET: api/SecureFiles
		[HttpGet]
		[Authorize]
        public IActionResult GetFiles()
        {
			var role = User.Claims.FirstOrDefault(x => x.Type.Equals("Role")).Value;
			if(role=="Admin"){
				var result = context.SecureFiles.Select(a=>new {
					id = a.Id,
					filename = a.Filename,
					description = a.Description,
					order = a.Order
				});
				return Ok(result);
			}
			return Unauthorized();
        }
		
        // POST: api/SecureFiles
		[HttpPost("download/{secretKey}")]
		[Authorize]
        public async Task<IActionResult> PostFiles([FromRoute] int secretKey, [FromBody] SecureFiles files)
        {
			var password = User.Claims.FirstOrDefault(x => x.Type.Equals("Password")).Value;
			var totp = this.totpGenerator.Generate(password);
			bool isValid = totpValidator.Validate(password, secretKey, 60);
            if (isValid)
            {
                try
				{
					var filename = files.Filename;
					if (filename == null)
						return Content("filename not present");

					var pathDir = Path.Combine(Directory.GetCurrentDirectory(), "Secure");
					var pathDirFile = Path.Combine(pathDir, filename);
					if(!System.IO.File.Exists(pathDirFile)){
						pathDir = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), "Secure");
						pathDirFile = Path.Combine(pathDir,filename);
					}
					if(!System.IO.File.Exists(pathDirFile)){
						pathDir = Path.Combine(GetApplicationRoot(), "Secure");
						pathDirFile = Path.Combine(pathDir,filename);
					}

					var memory = new MemoryStream();
					using (var stream = new FileStream(pathDirFile, FileMode.Open))
					{
						await stream.CopyToAsync(memory);
					}
					memory.Position = 0;
					return File(memory, GetContentType(pathDirFile), Path.GetFileName(pathDirFile));
				}
				catch (Exception ex)
				{
					return BadRequest(ex);
				}
            }
            return BadRequest();
        }
		
		private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
		
		private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
		}
		
		// PUT: api/SecureFiles/5
		[HttpPut("{id}")]
		[Authorize]
        public async Task<IActionResult> PutFiles([FromRoute] int id, [FromBody] SecureFiles files)
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
				context.SecureFiles.Update(files);

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
		
        // POST: api/SecureFiles
		[HttpPost]
		[Authorize]
        public async Task<IActionResult> PostFiles([FromBody] SecureFiles files)
        {
			var role = User.Claims.FirstOrDefault(x => x.Type.Equals("Role")).Value;
			var idUser = User.Claims.FirstOrDefault(x => x.Type.Equals("Id")).Value;
			if(role=="Admin"){
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				files.CreatedBy = Convert.ToInt32(idUser);
				files.CreatedDate = DateTime.Now;
				context.SecureFiles.Add(files);
				await context.SaveChangesAsync();

				return Ok(files);
			}
			return Unauthorized();
        }

        // DELETE: api/SecureFiles/5
		[HttpDelete("{id}")]
		[Authorize]
        public async Task<IActionResult> DeleteFiles([FromRoute] int id)
        {
			var role = User.Claims.FirstOrDefault(x => x.Type.Equals("Role")).Value;
			if(role=="Admin"){
				var files = context.SecureFiles.Where(a=>a.Id==id);
				if (files == null)
				{
					return NotFound();
				}
				try
				{
					foreach (var fl in files)
					{
						context.SecureFiles.Remove(fl);
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
