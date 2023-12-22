
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RouletteDemo.Api.Interfaces;

namespace RouletteDemo.Api.Models
{
    [Table("Bets")]
    public class Bet : IEntity
    {
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [Required]
        public string PlayerName { set; get; } = string.Empty;
        [Required]
        public decimal Amount { set; get; }
        [Required]
        public int Number   { set; get; }
        [Required]
        public DateTime DateCreated { set; get; } = DateTime.UtcNow;
    }
}