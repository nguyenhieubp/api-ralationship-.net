using RelationShip.Dto;

namespace RelationShip.Response.User
{
    public class ProfileUser
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Ratting { get; set; }
        public AddressDto Address { get; set; }
        public CardDto Card { get; set; }
    }
}
