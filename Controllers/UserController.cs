using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portal.Models;

namespace Portal.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
		private frontendContext context;
		public UserController(){
			context = new frontendContext();
		}
 
        [HttpGet("check")]		
        public IActionResult CheckUser()
        {
			return Ok(false);
        }
		
        [HttpGet("logout")]		
        public IActionResult Logout()
        {
			return Ok(false);
        }
    }
}
