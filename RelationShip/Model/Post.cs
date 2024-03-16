using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationShip.Model
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Column]
        public string? Title { get; set; }

        [Column]
        public string? Description { get; set; }

        [Column]
        public double? Ratting { get; set; }

        public User? User { get; set; }

        [Column]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public List<CategoryPost>? CategoryPosts { get; set; }

    }
}
