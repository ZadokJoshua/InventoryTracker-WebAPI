using InventoryTracker.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventoryTracker.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public record AuthenticationData(string? Username, string? Password);
        public record UserData(int Id, string? FirstName, string? LastName, string? Username);

        [HttpPost("token")]
        [AllowAnonymous]
        public ActionResult<TokenModel> Authenticate([FromBody] AuthenticationData data)
        {
            var user = ValidateCredentials(data);

            if(user is null)
            {
                return Unauthorized();
            }

            var token = GenerateToken(user);

            return Ok(new TokenModel
            {
                Token = token,
                Expires = DateTime.UtcNow.AddMinutes(60)
            });
        }

        private string GenerateToken(UserData user)
        {
            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretKey"]));

            SigningCredentials signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new();
            claims.Add(new(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            claims.Add(new(JwtRegisteredClaimNames.UniqueName, user.Username!));
            claims.Add(new(JwtRegisteredClaimNames.GivenName, user.FirstName!));
            claims.Add(new(JwtRegisteredClaimNames.FamilyName, user.LastName!));

            var token = new JwtSecurityToken(_configuration["Authentication:Issuer"], _configuration["Authentication:Audience"], claims, DateTime.UtcNow, DateTime.UtcNow.AddMinutes(60), signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserData? ValidateCredentials(AuthenticationData data)
        {
            // TO-DO: Not production code
            if (CompareValues(data.Username, "Zadok") && CompareValues(data.Password, "Zadok123"))
            {
                return new UserData(1, "Zadok", "Joshua", data.Username);
            }
            return null;
        }

        private bool CompareValues(string? actual, string? expected)
        {
            if (actual is not null && actual == expected)
            {
                return true;
            }

            return false;
        }
    }
}
