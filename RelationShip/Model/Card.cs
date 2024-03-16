using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationShip.Model
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }

        [Column]
        [Required]
        public string? NumberCard { get; set; }

        [Column]
        public string? Gender { get; set; }

        public User? User { get; set; }
    }
}
