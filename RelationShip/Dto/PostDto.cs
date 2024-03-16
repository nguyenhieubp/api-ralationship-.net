using RelationShip.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RelationShip.Dto
{
    public class PostDto
    {
        public int PostId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public double Ratting {  get; set; }
    }
}
