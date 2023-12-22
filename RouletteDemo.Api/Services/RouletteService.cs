using RouletteDemo.Api.Data.Interfaces;
using RouletteDemo.Api.Dtos;
using RouletteDemo.Api.Models;

namespace RouletteDemo.Api.Services
{
    public class RouletteService : IRouletteService
    {
        private readonly IDataProcessor _dataProcessor;

        public RouletteService(IDataProcessor dataProcessor)
        {
            _dataProcessor = dataProcessor;
        }

        /// <summary>
        ///  Allows users to place a bet.
        /// </summary>
        /// <param name="betDto"></param>
        /// <param name="token"></param>
        public async Task<ServiceResponse<BetDto>> PlaceBetAsync(BetDto betDto, CancellationToken token)
        {
            return await _dataProcessor.PlaceBetAsync(betDto, token).ConfigureAwait(false);
        }
        /// <summary>
        ///  Generates a random number between 0 and 36.
        /// </summary>
        /// <param name="token"></param>
        public async Task<ServiceResponse<SpinDto>> SpinAsync(CancellationToken token)
        {           
            return await _dataProcessor.SpinAsync(token).ConfigureAwait(false);
        }
        /// <summary>
        ///  Determines whether a player has won based on their previous bets and the latest spin result.
        /// </summary>
        /// <param name="token"></param>
        public async Task<ServiceResponse<List<PayoutDto>>> PayoutAsync(CancellationToken token)
        {            
             return await _dataProcessor.PayoutAsync(token).ConfigureAwait(false);
        }

        /// <summary>    
        ///  Gets a list of spins.
        /// </summary>
        /// <param name="token"></param>
        public async Task<ServiceResponse<List<SpinDto>>> ShowPreviousSpinsAsync(CancellationToken token)
        {
            return await _dataProcessor.ShowPreviousSpinsAsync(token).ConfigureAwait(false);
        }
    }
}