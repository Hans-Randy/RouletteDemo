using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RouletteDemo.Api.Interfaces;

namespace RouletteDemo.Api.Models
{
    [Table("Spins")]
    public class Spin : IEntity
    {
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }
        [Required]
        public int Number { set; get; }
        [Required]
        public DateTime DateCreated { set; get; } = DateTime.UtcNow;
    }
}