using AutoMapper;
using RouletteDemo.Api.Dtos;
using RouletteDemo.Api.Models;

namespace RouletteDemo.Api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BetDto,Bet>();
            CreateMap<Spin,SpinDto>();
            CreateMap<Bet, PayoutDto>();
        }
    }
}