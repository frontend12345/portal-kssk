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
    }
}
