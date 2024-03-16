using System.ComponentModel.DataAnnotations.Schema;

namespace RelationShip.Dto
{
    public class AuthDto
    {
        public int AuthId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int UserId { get; set; }
    }
}
