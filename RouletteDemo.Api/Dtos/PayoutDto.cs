
namespace RouletteDemo.Api.Dtos
{
    public class PayoutDto
    {
        public string PlayerName { set; get; } = string.Empty;
        public int BetNumber { set; get; }
        public decimal BetAmount {set; get; }
        public int LastSpinNumber { set; get; }
        public decimal PayoutAmount { set; get; }
    }
}