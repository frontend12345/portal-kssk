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

        [HttpGet]		
        public IActionResult GetMenu()
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
    }
}
