using Common.Models;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CurrencyAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IUserServices userServices, IConfiguration configuration)
        {
            _userServices = userServices;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult AuthUser([FromBody] CredentialsDTO credentialsDTO)
        { 
            User? user = _userServices.AuthUser(credentialsDTO);
            
            if (user == null)
            {
                return Unauthorized("Username or Password are incorrect");
            }

            SymmetricSecurityKey securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]!));
            SigningCredentials credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            List<Claim> claimsForToken = new List<Claim>()
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("given_name", user.Username),
                new Claim("conversions", user.Conversions.ToString()),
                new Claim("subscriptionId", user.SubscriptionId.ToString())
            };

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
              _configuration["Authentication:Issuer"],
              _configuration["Authentication:Audience"],
              claimsForToken,
              DateTime.UtcNow,
              DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Authentication:MinToExpireJWT"]!)),
              credentials);


            return Ok(new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken));

        }
    }
}
