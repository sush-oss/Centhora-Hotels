using Centhora_Hotels.InternalServices.CenthoraAuth;
using Centhora_Hotels.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Centhora_Hotels.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class CenthoraController : ControllerBase
    {
        private readonly ICenthoraAuth centhoraAuth;
        private readonly IConfiguration configuration;

        public CenthoraController(ICenthoraAuth _auth, IConfiguration _configuration)
        {
            centhoraAuth = _auth;
            configuration = _configuration;
        }

        // Login
        [AllowAnonymous]
        [HttpPost]
        [Route("api/[controller]/centhoralogin")]
        public ActionResult CenthoraUserLogin(CenthoraAuthDto authDto)
        {
            var logingUser = centhoraAuth.AuthenticateUser(authDto.UserName, authDto.UserPassword);
            if (logingUser == null)
            {
                return BadRequest(new { message = "Invalid username or password" });
            }

            // Generate a JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["JWT:Key"]);
            var issure = configuration["JWT:Issure"];
            var audience = configuration["JWT:Audience"];
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, logingUser.UserName)
                }),
                Issuer = issure,
                Audience = audience,
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new {Token =  tokenString});
        }
        
    }
}
