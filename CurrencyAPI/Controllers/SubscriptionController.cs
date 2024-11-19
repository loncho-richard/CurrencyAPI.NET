using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Security.Claims;

namespace CurrencyAPI.Controllers
{
    [Route("api/subscription")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        public readonly ISubscriptionServices _subscriptionServices;
        public SubscriptionController(ISubscriptionServices subscriptionServices)
        {
            _subscriptionServices = subscriptionServices;
        }
        [HttpPost]
        [Authorize]
        public IActionResult CreateSubscription([FromBody] SubscriptionDTO newSubscriptionDTO)
        {
            try
            {
                int newSubscriptionId = _subscriptionServices.CreateSubscription(newSubscriptionDTO);
                return Ok($"Subscription with id: {newSubscriptionId} has been created");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_subscriptionServices.GetAll());
        }

        [HttpGet("{subscriptionId}")]
        public IActionResult GetOne([FromRoute] int subscriptionId)
        {
            try 
            {
                return Ok(_subscriptionServices.GetOne(subscriptionId));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }

        }

        [HttpPut("{subscriptionId}")]
        [Authorize]
        public IActionResult UpdateSubscription([FromRoute] int subscriptionId, [FromBody] SubscriptionDTO subscription)
        {
            try
            {
                return Ok(_subscriptionServices.UpdateSubscription(subscriptionId, subscription));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpDelete("{subscriptionId}")]
        [Authorize]
        public IActionResult DeleteSubscription([FromRoute] int subscriptionId)
        {
            try
            {
                _subscriptionServices.DeleteSubscription(subscriptionId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("{subscriptionId}")]
        [Authorize]
        public IActionResult AssignSubscription([FromRoute] int subscriptionId)
        {
            try
            {
                var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value.ToString()!);
                _subscriptionServices.AssignSubscription(userId, subscriptionId);
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
