using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

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
    }
}
