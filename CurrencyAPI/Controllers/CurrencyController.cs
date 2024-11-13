using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace CurrencyAPI.Controllers
{
    [Route("api/currency")]
    [ApiController]
    [Authorize]
    public class CurrencyController : ControllerBase
    {
        public readonly ICurrencyServices _currencyServices;
        public CurrencyController(ICurrencyServices currencyServices)
        {
            _currencyServices = currencyServices;
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
                return BadRequest($"Error: {ex}");
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
                return BadRequest($"Error: {ex}");
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
                return BadRequest($"Error: {ex}");
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
                return BadRequest($"Error {ex}");
            }
        }
    }
}
