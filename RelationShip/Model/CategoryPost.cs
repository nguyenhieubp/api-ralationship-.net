using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationShip.Model
{
    public class CategoryPost
    {
        [Key]
        public int CategoryPostId { get; set; }

        [Column]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        
        public Category? Categories { get; set; }

        [Column]
        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }

        public Post? Posts { get; set; }
    }
}
