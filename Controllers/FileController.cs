using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portal.Models;

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
        public IQueryable<Files> GetFiles()
        {
            return context.Files;
        }
		
		// PUT: api/Files/5
		[HttpPut("{id}")]
        public async Task<IActionResult> PutFiles([FromRoute] int id,[FromBody] Files files)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != files.Id)
            {
                return BadRequest();
            }

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
		
        // POST: api/Files
		[HttpPost]
        public async Task<IActionResult> PostFiles([FromBody] Files files)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            files.CreatedBy = 1;
            files.CreatedDate = DateTime.Now;
            context.Files.Add(files);
            await context.SaveChangesAsync();

            return Ok(files);
        }

        // DELETE: api/Files/5
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFiles([FromRoute] int id)
        {
            var files = context.Files.Where(a=>a.Id==id);
            if (files == null)
            {
                return NotFound();
            }
            try
            {
                foreach (var file in files)
                {
                    context.Files.Remove(file);
                }
                await context.SaveChangesAsync();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }

            return Ok(files);
        }
    }
}
