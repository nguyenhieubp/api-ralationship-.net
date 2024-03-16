using System.ComponentModel.DataAnnotations.Schema;

namespace RelationShip.Dto
{
    public class CardDto
    {
        public int CardId { get; set; }

        public string? NumberCard { get; set; }

        public string? Gender { get; set; }
    }
}
