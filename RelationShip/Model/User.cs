using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationShip.Model
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Column]
        public string? FirstName { get; set; }
        
        [Column]
        public string? LastName { get; set; }

        [Column]
        [Range(0, 5, ErrorMessage = "Ratting must be between 0 and 5.")]
        public double Ratting { get; set; }

        [Column]
        [ForeignKey(nameof(User))]
        public int AddressId { get; set; }

        public Address Address { get; set; } = null!;
      
        [Column]
        [ForeignKey(nameof(User))]
        public int CardId { get; set; }

        public Card? Card { get; set; } 
        
        public Auth Auth { get; set; }

        public ICollection<Post>? Posts { get; set; }

        public ICollection<Messages>? Messages { get; set; }
    }
}
