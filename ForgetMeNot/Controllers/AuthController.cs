using ForgetMeNot.Data;
using ForgetMeNot.Libraries;
using ForgetMeNot.Models;
using ForgetMeNot.Models.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ForgetMeNot.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly IConfiguration _config;
        private readonly AppDbContext _context;

        public AuthController(IConfiguration config, AppDbContext context)
        { 
            _config = config;
            _context = context;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(AuthDto dto)
        {
            // check if user already exists with username
            if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
            {
                return BadRequest("Username already exists");
            }

            var authentication = new Authentication();
            authentication.CreatePasswordHash(dto.Password, out byte[] hash, out byte[] salt);

            // create new user
            var newUser = new User
            {
                Username = dto.Username,
                PasswordHash = hash,
                PasswordSalt = salt
            };

            // save new user to db
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok("User registered");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(AuthDto dto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == dto.Username);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }
            var authentication = new Authentication();            
            if (!authentication.VerifyPasswordHash(dto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized("Invalid credentials");
            }

            var jwtSettings = new JwtSettings
            {
                SigningKey = _config["Jwt:SigningKey"]!,
                Issuer = _config["Jwt:Issuer"]!,
                Audience = _config["Jwt:Audience"]!
            };
            var token = authentication.GenerateJwtToken(user, jwtSettings);

            return Ok(new { token });
        }
    }
}
