using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;  
using Portal.Models;
using AspNetCore.Totp;

namespace Portal.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
		private frontendContext context;
        private IConfiguration _configuration { get; set; }
		private readonly TotpSetupGenerator totpSetupGenerator;
		
		public UserController(frontendContext con, IConfiguration configuration){
			context = con;
			_configuration = configuration;
			totpSetupGenerator = new TotpSetupGenerator();
		}
		
        [HttpGet("qrcode")]	
		[Authorize]
        public IActionResult GetQRCode()
        {
			var username = User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name)).Value;
			var password = User.Claims.FirstOrDefault(x => x.Type.Equals("Password")).Value;
			var totpSetup = this.totpSetupGenerator.Generate("Portal KSSK", username, password);
            return Ok(totpSetup.QrCodeImage);
        }
		
		public string ComputeSha256Hash(string rawData)  
        {  
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())  
            {  
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));  
  
                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();  
                for (int i = 0; i < bytes.Length; i++)  
                {  
                    builder.Append(bytes[i].ToString("x2"));  
                }  
                return builder.ToString();  
            }  
        } 
		
		[HttpPost("login")]
        public IActionResult Authenticate([FromBody]User userLogin)
        {
			var sha256Pass = this.ComputeSha256Hash(userLogin.Password);
            var user = context.User.SingleOrDefault(x => ((x.Username == userLogin.Username) && (x.Password == sha256Pass.ToUpper())));

            if (user == null)
                return NotFound();

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["ConnectionStrings:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim("Password", user.Password.ToString()),
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Role", user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Authenticator = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;
            return Ok(user);
        }
		
		// GET: api/User
		[HttpGet]
		[Authorize]
        public IActionResult GetUser()
        {
			var role = User.Claims.FirstOrDefault(x => x.Type.Equals("Role")).Value;
			if(role=="Admin"){
				var result = context.User.Select(a=>new {
					id = a.Id,
					username = a.Username,
					password = a.Password,
					role = a.Role,
					authenticator = a.Authenticator
				});
				return Ok(result);
			}
			return Unauthorized();
        }
		
		// PUT: api/User/5
		[HttpPut("{id}")]
		[Authorize]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        {
			var role = User.Claims.FirstOrDefault(x => x.Type.Equals("Role")).Value;
			var idUser = User.Claims.FirstOrDefault(x => x.Type.Equals("Id")).Value;
			if(role=="Admin"){
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
			return Unauthorized();
        }
		
        // POST: api/User
		[HttpPost]
		[Authorize]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
			var role = User.Claims.FirstOrDefault(x => x.Type.Equals("Role")).Value;
			var idUser = User.Claims.FirstOrDefault(x => x.Type.Equals("Id")).Value;
			if(role=="Admin"){
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				context.User.Add(user);
				await context.SaveChangesAsync();

				return Ok(user);
			}
			return Unauthorized();
        }

        // DELETE: api/User/5
		[HttpDelete("{id}")]
		[Authorize]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
			var role = User.Claims.FirstOrDefault(x => x.Type.Equals("Role")).Value;
			if(role=="Admin"){
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
			return Unauthorized();
        }
    }
}
