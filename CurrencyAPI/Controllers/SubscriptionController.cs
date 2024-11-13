using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace CurrencyAPI.Controllers
{
    [Route("api/subscription")]
    [ApiController]
    [Authorize]
    public class SubscriptionController : ControllerBase
    {
        public readonly ISubscriptionServices _subscriptionServices;
        public SubscriptionController(ISubscriptionServices subscriptionServices)
        {
            _subscriptionServices = subscriptionServices;
        }

        [HttpPost]
        public IActionResult CreateSubscription([FromBody] SubscriptionDTO newSubscriptionDTO)
        {
            try
            {
                int newSubscriptionId = _subscriptionServices.CreateSubscription(newSubscriptionDTO);
                return Ok($"Subscription with id: {newSubscriptionId} has been created");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex}");
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
                return BadRequest($"Error: {ex}");
            }

        }

        [HttpPut("{subscriptionId}")]
        public IActionResult UpdateSubscription([FromRoute] int subscriptionId, [FromBody] SubscriptionDTO subscription)
        {
            try
            {
                return Ok(_subscriptionServices.UpdateSubscription(subscriptionId, subscription));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex}");
            }
        }

        [HttpDelete("{subscriptionId}")]
        public IActionResult DeleteSubscription([FromRoute] int subscriptionId)
        {
            try
            {
                _subscriptionServices.DeleteSubscription(subscriptionId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex}");
            }
        }
    }
}
