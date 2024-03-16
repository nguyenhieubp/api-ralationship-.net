using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationShip.Model
{
    public class Auth
    {
        [Key]
        public int AuthId { get; set; }
        
        [Column]
        public string Email { get; set; }
        
        [Column]
        public string Password { get; set; }

        [Column]
        public string RefreshToken { get; set; }

        public User  User  { get; set; }

        public int UserId { get; set; }

        public OTP? Otp { get; set; }
    }
}
