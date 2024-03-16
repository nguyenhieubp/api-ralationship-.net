using System.ComponentModel.DataAnnotations.Schema;

namespace RelationShip.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public double Ratting {  get; set; }   
    }
}
