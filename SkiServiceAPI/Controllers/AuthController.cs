using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SkiServiceAPI.Data;
using SkiServiceAPI.Models;
using SkiServiceAPI.Services;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens;


namespace SkiServiceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public AuthController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            if (await _context.User.AnyAsync(u => u.UserName == user.UserName))
                return BadRequest("The username already exists");

            user.SetPassword(user.Passwort);
            await _context.Users.InsertOneAsync(user);

            return Ok("User successfully registered.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequest request)
        {
            var user = await _context.Users.Find(u => u.UserName == request.UserName).FirstOrDefaultAsync();
            if (user == null || !user.VerifyPassword(request.Passwort))
                return Unauthorized("Invalid login data.");

            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKey123!")); // Cambia esto a una clave segura.
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "yourapp.com",
                audience: "yourapp.com",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
