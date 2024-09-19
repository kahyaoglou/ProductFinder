using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProductFinder.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductFinder.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;
        public AuthController(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("Giris")]
        public IActionResult Login([FromBody]User user)
        {
            var apiUser = AuthUser(user);
            if(apiUser == null) return NotFound("Kullanıcı bulunamadı");

            var token = CreateToken(user);
            return Ok(token);
        }

        private string CreateToken(User user)
        {
            if (_jwtSettings.Key == null) throw new Exception("Jwt ayarlarındaki Key değeri null olamaz.");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimDizisi = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username!),
                new Claim(ClaimTypes.Role, user.Role!)
            };

            var token = new JwtSecurityToken(_jwtSettings.Issuer,
                _jwtSettings.Audience,
                claimDizisi,
                expires:  DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User? AuthUser(User user)
        {
            return Users
                .User
                .FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);
        }
    }
}
