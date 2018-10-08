using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portal.Models;

namespace Portal.Controllers
{
    [Route("api/[controller]")]
    public class MenuController : Controller
    {
		private frontendContext context;
		public MenuController(){
			context = new frontendContext();
		}

        [HttpGet("navigation")]		
        public IActionResult GetNavigation()
        {
			dynamic result;
			try{
				result = context.Menu.Where(a=>a.IsActive==true).OrderBy(a=>a.Order).Select(a=>new {
					id = a.Id,
					parentId = a.ParentId,
					title = a.Title,
					isActive = a.IsActive,
					order = a.Order,
					url = a.Url,
					mode = a.Mode
				});
			}catch(Exception ex){
				return BadRequest(ex.Message);
			}
			return Ok(result);
        }
		
		// GET: api/Menu
		[HttpGet]
        public IQueryable<Menu> GetMenu()
        {
            return context.Menu;
        }
		
		// PUT: api/Menu/5
		[HttpPut("{id}")]
        public async Task<IActionResult> PutMenu([FromRoute] int id, [FromBody] Menu menu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != menu.Id)
            {
                return BadRequest();
            }

            context.Menu.Update(menu);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(menu);
        }
		
        // POST: api/Menu
		[HttpPost]
        public async Task<IActionResult> PostMenu([FromBody] Menu menu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            menu.CreatedBy = 1;
            menu.CreatedDate = DateTime.Now;
            context.Menu.Add(menu);
            await context.SaveChangesAsync();

            return Ok(menu);
        }

        // DELETE: api/Menu/5
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu([FromRoute] int id)
        {
            var menu = context.Menu.Where(a=>a.Id==id);
            if (menu == null)
            {
                return NotFound();
            }
            try
            {
                foreach (var mn in menu)
                {
                    context.Menu.Remove(mn);
                }
                await context.SaveChangesAsync();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }

            return Ok(menu);
        }
    }
}
