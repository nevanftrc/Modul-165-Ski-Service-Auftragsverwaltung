using Microsoft.AspNetCore.Mvc;
using SkiServiceAPI.Data;
using SkiServiceAPI.Models;

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

            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return Ok("User successfully registered.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequest request)
        {
            var user = await _context.User.SingleOrDefaultAsync(u => u.UserName == request.UserName);
            if (user == null || user.Passwort != request.Passwort)
                return Unauthorized("Invalid login data.");

            return Ok("Successfully registered.");
        }
    }
}
