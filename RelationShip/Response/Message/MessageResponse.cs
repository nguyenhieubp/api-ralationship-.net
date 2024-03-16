using RelationShip.Dto;

namespace RelationShip.Response.Message
{
    public class MessageResponse 
    {
        public int MessageId { get; set; }

        public string? Message { get; set; }

        public UserDto user { get; set; }
    }
}
