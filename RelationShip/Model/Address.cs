using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationShip.Model
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        [Column]
        public string? City { get; set; }

        public User? User { get; set; }
    }
}
