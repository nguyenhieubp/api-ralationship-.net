using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationShip.Model
{
    public class Messages
    {
        [Key]
        public int MessageId { get; set; }

        [Column]
        public int UserId { get; set; }

        public User? User { get; set; }

        [Column]
        public string? Message { get; set; } 

    }
}
