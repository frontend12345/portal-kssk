using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portal.Models;

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
				if(type == "full"||type == "single"){
					result = context.Content.Where(a=>a.Url==url && a.IsActive==true).Select(a=>new {
						id = a.Id,
						menuid = a.MenuId,
						url = a.Url,
						content = a.Content1,
						isactive = a.IsActive
					}).FirstOrDefault();
				}else if(type == "multiplefoto"||type == "multiplefile"||type == "multipletext"){
					result = context.Content.Where(a=>a.Url==url && a.IsActive==true).Select(a=>new {
						id = a.Id,
						menuid = a.MenuId,
						url = a.Url,
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
        public IQueryable<Content> GetContent()
        {
            return context.Content;
        }
		
		// PUT: api/Content/5
		[HttpPut("{id}")]
        public async Task<IActionResult> PutContent([FromRoute] int id,[FromBody] Content content)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != content.Id)
            {
                return BadRequest();
            }

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
		
        // POST: api/Content
		[HttpPost]
        public async Task<IActionResult> PostContent([FromBody] Content content)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            content.CreatedBy = 1;
            content.CreatedDate = DateTime.Now;
            context.Content.Add(content);
            await context.SaveChangesAsync();

            return Ok(content);
        }

        // DELETE: api/Content/5
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContent([FromRoute] int id)
        {
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
    }
}
