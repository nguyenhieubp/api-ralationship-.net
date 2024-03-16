using Microsoft.EntityFrameworkCore;
using RelationShip.Model;

namespace RelationShip.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryPost> CategoryPosts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Card> Card { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Auth> Auths { get; set; }
        public DbSet<OTP> Otps { get; set; }

    }
}
