using AutoMapper;
using RouletteDemo.Api.Dtos;
using RouletteDemo.Api.Interfaces;
using RouletteDemo.Api.Models;

namespace RouletteDemo.Api.Services
{
    public class RouletteService : IRouletteService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Bet> _betRepository;
        private readonly IRepository<Spin> _spinRepository;

        public RouletteService(IMapper mapper, IRepository<Bet> betRepository, IRepository<Spin> spinRepository)
        {
            _mapper = mapper;
            _betRepository = betRepository;
            _spinRepository = spinRepository;
        }

        /// <summary>
        ///  Allows users to place a bet.
        /// </summary>
        /// <param name="betDto"></param>
        /// <param name="cancellationToken"></param>
        public async Task<ServiceResponse<BetDto>> PlaceBetAsync(BetDto betDto, CancellationToken cancellationToken)
        {
            var response = new ServiceResponse<BetDto>();

            if (betDto == null || betDto.Amount <= 0 || string.IsNullOrEmpty(betDto.PlayerName))
                throw new RequestException("Invalid bet request");

            var bet = _mapper.Map<Bet>(betDto);

            await _betRepository.AddAsync(bet, cancellationToken);
            
            response.Data = betDto;
            return response;
        }
        /// <summary>
        ///  Generates a random number between 0 and 36.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public async Task<ServiceResponse<SpinDto>> SpinAsync(CancellationToken cancellationToken)
        {   
            var bets = await _betRepository.GetAllAsync(cancellationToken);

            if (!bets.Any())
                throw new RouletteException("No bet was placed.");

            var spinModel = new Spin
            {
                Number = new Random().Next(0, 36)
            };

            await _spinRepository.AddAsync(spinModel, cancellationToken);
                        
            return new ServiceResponse<SpinDto>
            { 
                Data = _mapper.Map<SpinDto>(spinModel) 
            };

        }
        /// <summary>
        ///  Determines whether a player has won based on their previous bets and the latest spin result.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public async Task<ServiceResponse<List<PayoutDto>>> PayoutAsync(CancellationToken cancellationToken)
        {            
            var allBets = await _betRepository.GetAllAsync(cancellationToken);

            if (!allBets.Any())
                throw new RouletteException("No bet was placed.");

            var lastSpin = await _spinRepository.GetLastItem(cancellationToken);
            
            if (lastSpin is null)
                throw new RouletteException("The wheel was not spined.");

            var winingBets = _betRepository.GetItems(bet => bet.Number == lastSpin.Number);

            var response = new ServiceResponse<List<PayoutDto>>
            {
                Data = new List<PayoutDto>()
            };

            if (winingBets is null || !winingBets.Any())
            {
                response.Data.AddRange(allBets.Select(loosingBet => new PayoutDto 
                {
                    PlayerName = loosingBet.PlayerName,
                    BetNumber = loosingBet.Number,
                    BetAmount = loosingBet.Amount,
                    LastSpinNumber = lastSpin.Number
                }));

                response.Message = "No winners this round.";
                return response;
            }

            response.Data.AddRange(winingBets.Select(winingBet => new PayoutDto 
            {
                PlayerName = winingBet.PlayerName,
                BetNumber = winingBet.Number,
                BetAmount = winingBet.Amount,
                LastSpinNumber = lastSpin.Number,
                PayoutAmount = 35 * winingBet.Amount
            }));
            
            foreach(var bet in allBets)
            {
                await _betRepository.DeleteAsync(bet, cancellationToken);
            }

            response.Message = "These are the winners.";
            return response;
        }

        /// <summary>    
        ///  Gets a list of spins.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public async Task<ServiceResponse<List<SpinDto>>> ShowPreviousSpinsAsync(CancellationToken cancellationToken)
        {
            var spins = await _spinRepository.GetAllAsync(cancellationToken);

            return new ServiceResponse<List<SpinDto>>
            {
                Data = spins?.Select(c => _mapper.Map<SpinDto>(c))?.ToList() ?? null
            };
        }
    }
}