using RelationShip.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationShip.Dto
{
    public class MessageDto
    {
        public int MessageId { get; set; }

        public int UserId { get; set; }

        public string? Message { get; set; }

    }
}
