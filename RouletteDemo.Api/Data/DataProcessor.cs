using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RouletteDemo.Api.Data.Interfaces;
using RouletteDemo.Api.Dtos;
using RouletteDemo.Api.Models;

namespace RouletteDemo.Api.Data
{
    public class DataProcessor : IDataProcessor
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public DataProcessor(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        ///  Allows users to place a bet.
        /// </summary>
        /// <param name="betDto"></param>
        /// <param name="token"></param>
        public async Task<ServiceResponse<BetDto>> PlaceBetAsync(BetDto betDto, CancellationToken token)
        {
            var response = new ServiceResponse<BetDto>();

            if (betDto == null || betDto.Amount <= 0 || string.IsNullOrEmpty(betDto.PlayerName))
                throw new RequestException("Invalid bet request");

            var bet = _mapper.Map<Bet>(betDto);

            await _context.Bets
                .AddAsync(bet, token)
                .ConfigureAwait(false);
            
            await _context
                .SaveChangesAsync(token)
                .ConfigureAwait(false);

            response.Data = betDto;
            return response;
        }
        /// <summary>
        ///  Generates a random number between 0 and 36.
        /// </summary>
        /// <param name="token"></param>
        public async Task<ServiceResponse<SpinDto>> SpinAsync(CancellationToken token)
        {
            var bets = await GetBets(token)
                    .ConfigureAwait(false);

            if (bets.Count == 0)
                throw new RouletteException("No bet was placed.");

            var spinModel = new Spin
            {
                Number = new Random().Next(0, 36)
            };

            await _context.Spins
                    .AddAsync(spinModel, token)
                    .ConfigureAwait(false);
            
            await _context
                    .SaveChangesAsync(token)
                    .ConfigureAwait(false);

            var response = new ServiceResponse<SpinDto>
            { 
                Data = _mapper.Map<SpinDto>(spinModel) 
            };
            
            return response;
        }
        /// <summary>
        ///  Determines whether a player has won based on their previous bets and the latest spin result.
        /// </summary>
        /// <param name="token"></param>
        public async Task<ServiceResponse<List<PayoutDto>>> PayoutAsync(CancellationToken token)
        {            
            var bets = await GetBets(token)
                    .ConfigureAwait(false);

            if (bets.Count == 0)
                throw new RouletteException("No bet was placed.");

            var latestSpin = await _context.Spins
                .AsNoTracking()
                .OrderByDescending(spin => spin.Id)
                .LastOrDefaultAsync(token)
                .ConfigureAwait(false);
            
            if (latestSpin is null)
                throw new RouletteException("The wheel was not spined.");

            bets = await _context.Bets
                .Where(bet => bet.Number == latestSpin.Number)
                .AsNoTracking()
                .ToListAsync(token)
                .ConfigureAwait(false);

            var response = new ServiceResponse<List<PayoutDto>>();

            if (bets is null || bets.Count == 0)
            {
                response.Message = "No winners this round.";
                return response;
            }

            response.Data = new List<PayoutDto>();

            foreach(var bet in bets)
            {
                response.Data.Add(new PayoutDto 
                {
                    PlayerName = bet.PlayerName,
                    Number = latestSpin.Number,
                    Amount = 35 * bet.Amount
                });

                _context.Remove(bet);
                await _context.SaveChangesAsync(token);
            }

            return response;
        }

        /// <summary>    
        ///  Gets a list of spins.
        /// </summary>
        /// <param name="token"></param>
        public async Task<ServiceResponse<List<SpinDto>>> ShowPreviousSpinsAsync(CancellationToken token)
        {
            var spins = await _context.Spins
                .AsNoTracking()
                .ToListAsync(token)
                .ConfigureAwait(false);

            var response = new ServiceResponse<List<SpinDto>>
            {
                Data = spins?.Select(c => _mapper.Map<SpinDto>(c))?.ToList() ?? null
            };
            return response;
        }

        private async Task<List<Bet>> GetBets(CancellationToken token)
        {
            return await _context.Bets
                .AsNoTracking()
                .ToListAsync(token)
                .ConfigureAwait(false);
        }
    }
}