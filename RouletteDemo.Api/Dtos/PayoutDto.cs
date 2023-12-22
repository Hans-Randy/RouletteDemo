
namespace RouletteDemo.Api.Dtos
{
    public class PayoutDto
    {
        public string PlayerName { set; get; } = string.Empty;
        public int Number { set; get; }
        public decimal Amount { set; get; }
    }
}