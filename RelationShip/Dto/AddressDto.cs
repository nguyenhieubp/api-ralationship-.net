using RelationShip.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationShip.Dto
{
    public class AddressDto
    {
        public int AddressId { get; set; }

        public string? City { get; set; }
    }
}
