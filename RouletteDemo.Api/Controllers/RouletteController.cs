using Microsoft.AspNetCore.Mvc;
using RouletteDemo.Api.Dtos;
using RouletteDemo.Api.Models;
using RouletteDemo.Api.Services;

namespace RouletteDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : ControllerBase
    {
        private readonly IRouletteService _rouletteService;
        public RouletteController(IRouletteService rouletteService)
        {
            _rouletteService = rouletteService;
        }

        /// <summary>
        ///  Allows users to place a bet.
        /// </summary>
        /// <param name="betRequestDto"></param>
        [HttpPost("placebet")]
        public async Task<ActionResult<ServiceResponse<BetDto>>> PlaceBet([FromBody] BetDto betRequest)
        {
            return Ok(await _rouletteService.PlaceBetAsync(betRequest, CancellationToken.None));
        }

        /// <summary>
        ///  Generates a random number between 0 and 36.
        /// </summary>
        [HttpPost("spin")]
        public async Task<ActionResult<ServiceResponse<SpinDto>>> Spin()
        {
            return Ok(await _rouletteService.SpinAsync(CancellationToken.None));
        }

        /// <summary>
        ///  Determines whether a player has won based on their previous bets and the latest spin result.
        /// </summary>
        [HttpGet("payout")]
        public async Task<ActionResult<ServiceResponse<PayoutDto>>> Payout()
        {
            return Ok(await _rouletteService.PayoutAsync(CancellationToken.None));
        }

        /// <summary>    
        ///  Displays a list of previous spin results.
        /// </summary>
        [HttpGet("showpreviousspins")]
        public async Task<ActionResult<ServiceResponse<List<SpinDto>>>> ShowPreviousSpins()
        {
            return Ok(await _rouletteService.ShowPreviousSpinsAsync(CancellationToken.None));
        }
    }
}
