using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationShip.Model
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Column]
        public string? CategoryName { get; set; }

        public List<CategoryPost>? CategoryPosts { get; set; }

    }
}
