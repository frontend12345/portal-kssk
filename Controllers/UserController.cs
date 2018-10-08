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
		
		

		// GET: api/User
		[HttpGet]
        public IQueryable<User> GetUser()
        {
            return context.User;
        }
		
		// PUT: api/User/5
		[HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id,[FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            context.User.Update(user);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(user);
        }
		
        // POST: api/User
		[HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
			
            context.User.Add(user);
            await context.SaveChangesAsync();

            return Ok(user);
        }

        // DELETE: api/User/5
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var user = context.User.Where(a=>a.Id==id);
            if (user == null)
            {
                return NotFound();
            }
            try
            {
                foreach (var us in user)
                {
                    context.User.Remove(us);
                }
                await context.SaveChangesAsync();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }

            return Ok(user);
        }
    }
}
