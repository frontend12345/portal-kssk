using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
		
		[HttpPost("login")]
        public IActionResult Authenticate([FromBody]User userLogin)
        {
            var user = context.User.SingleOrDefault(x => ((x.Username == userLogin.Username) && (x.Password == userLogin.Password)));

            if (user == null)
                return Ok(false);

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("Hahahaha Bagooos!!");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Authenticator = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;
            return Ok(true);
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
		
        // POST: api/User
		[HttpPost("list")]
        public async Task<IActionResult> PostUserList([FromBody] List<User> user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
			
			foreach(var us in user){
				context.User.Add(us);
			}
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
