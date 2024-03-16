using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationShip.Model
{
    public class OTP
    {
        [Key]
        public int OTPId { get; set; }

        [Column]
        public string Code { get; set; }    
        
        [Column]
        public DateTime ExpirationTime { get; set; }


        [Column]
        public int AuthId { get; set; }

        public Auth Auth { get; set; } = null;
    }
}
