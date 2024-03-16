using RelationShip.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RelationShip.RequestBody.User
{
    public class NewUserRequest
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public decimal Ratting { get; set; } = 0;

        public int AddressId { get; set; }

        public int CardId { get; set; }
    }
}
