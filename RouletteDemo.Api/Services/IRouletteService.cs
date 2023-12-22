using RouletteDemo.Api.Dtos;
using RouletteDemo.Api.Models;

namespace RouletteDemo.Api.Services
{
    public interface IRouletteService
    {
        /// <summary>
        ///  Allows users to place a bet.
        /// </summary>
        /// <param name="betRequestDto"></param>
        /// <param name="token"></param>
        Task<ServiceResponse<BetDto>> PlaceBetAsync(BetDto betRequestDto, CancellationToken token);
        /// <summary>
        ///  Generates a random number between 0 and 36.
        /// </summary>
        /// <param name="token"></param>
        Task<ServiceResponse<SpinDto>> SpinAsync(CancellationToken token);
        /// <summary>
        ///  Determines whether a player has won based on their previous bets and the latest spin result.
        /// </summary>
        /// <param name="token"></param>
        Task<ServiceResponse<List<PayoutDto>>> PayoutAsync(CancellationToken token);
        /// <summary>
        ///  Displays a list of previous spin results.
        /// </summary>
        /// <param name="token"></param>
        Task<ServiceResponse<List<SpinDto>>> ShowPreviousSpinsAsync(CancellationToken token);  
    }
}