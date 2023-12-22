
namespace RouletteDemo.Api.Dtos
{
    public class BetDto
    {
        public string PlayerName { set; get; } = string.Empty;
        public decimal Amount { set; get; }
        public int Number   { set; get; }
    }
}