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
    }
}
