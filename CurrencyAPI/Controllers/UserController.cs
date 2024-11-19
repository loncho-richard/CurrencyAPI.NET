using Common.Models;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Security.Claims;

namespace CurrencyAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] NewUserDTO newUserDTO)
        {
            try
            {
                int newUserId = _userServices.CreateUser(newUserDTO);
                return Ok($"User white Id: {newUserId} has been created");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex}");
            }
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult GetOneUser() 
        {
            try
            {
                var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value.ToString()!);
                UserDetailDTO user = _userServices.GetOneById(userId);
                return Ok(user);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
