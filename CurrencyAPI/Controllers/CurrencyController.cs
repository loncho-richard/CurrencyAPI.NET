using Common.Models;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Security.Claims;

namespace CurrencyAPI.Controllers
{
    [Route("api/currency")]
    [ApiController]
    [Authorize]
    public class CurrencyController : ControllerBase
    {
        public readonly ICurrencyServices _currencyServices;
        public readonly ISubscriptionServices _subscriptionServices;
        public readonly IVerifyToConversionServices _verifyToConversionServices;
        public readonly IUserServices _userServices;
        public CurrencyController(
            ICurrencyServices currencyServices, 
            ISubscriptionServices subscriptionServices,
            IVerifyToConversionServices verifyToConversionServices,
            IUserServices userServices)
        {
            _currencyServices = currencyServices;
            _subscriptionServices = subscriptionServices;
            _verifyToConversionServices = verifyToConversionServices;
            _userServices = userServices;
        }

        [HttpPost]
        public IActionResult CreateCurrency([FromBody] CurrencyDTO newCurrencyDTO)
        {
            try
            {
                int newCurrencyId = _currencyServices.CreateCurrency(newCurrencyDTO);
                return Ok($"Currency with id: {newCurrencyId} has been created");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_currencyServices.GetAll());
        }

        [HttpGet("{currencyId}")]
        public IActionResult GetOne([FromRoute] int currencyId)
        {
            try
            {
                return Ok(_currencyServices.GetOne(currencyId));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPut("{currencyId}")]
        public IActionResult UdpateCurrency([FromRoute] int currencyId, [FromBody] CurrencyDTO currency)
        {
            try
            {
                return Ok(_currencyServices.UpdateCurrency(currencyId, currency));
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpDelete("{currencyId}")]
        public IActionResult DeleteCurrency([FromRoute] int currencyId)
        {
            try
            {
                _currencyServices.DeleteCurrency(currencyId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }

        [HttpPost("convert")]
        public IActionResult ConvertCurrency([FromBody] ConversionDTO conversionDTO)
        {
            
            try
            {
                var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value.ToString()!);
                return Ok(_verifyToConversionServices.VerifyToConversion(userId, conversionDTO));
            }
            catch (Exception ex) 
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

    }
}
