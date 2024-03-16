using RelationShip.Model;

namespace RelationShip.Dto
{
    public class OtpDto
    {
        public string? Code { get; set; } 

        public DateTime ExpirationTime { get; set; }

        public int AuthId { get; set; }
    }
}
