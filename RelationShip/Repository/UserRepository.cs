using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RelationShip.Data;
using RelationShip.Interfaces;
using RelationShip.Model;
using RelationShip.Response.User;

namespace RelationShip.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<User> GetUserById(int userID)
        {
            return await _db.Users.FirstOrDefaultAsync(user => user.UserId == userID);
        }

        public IEnumerable<User> GetUsers()
        {
            return _db.Users.OrderBy(user => user.UserId).ToList();
        }

        public async Task<IEnumerable<User>> RattingUSerASC()
        {
            return await _db.Users.OrderBy((user) => user.Ratting).ToListAsync();
        }

        public async Task<IEnumerable<User>> RattingUSerDESC()
        {
            return await _db.Users.OrderByDescending((user) => user.Ratting).ToListAsync();
        }

        public User CreateUser([FromBody] User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return user;
        }

        public bool UserExists(int userId)
        {
            return _db.Users.Any(user => user.UserId == userId);
        }

        public User UpdateUser([FromBody] User user, [FromRoute] int UserId)
        {
            _db.Users.Update(user);
            _db.SaveChanges();
            return user;
        }

        public bool DeleteUser(int UserId)
        {
            var user = _db.Users.FirstOrDefault((user) => user.UserId == UserId);
            _db.Users.Remove(user);
            _db.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<Post>> GetAllPostOfUser(int userId)
        {
            var userWithPosts = await _db.Posts
                .Where(post => post.User.UserId == userId)
                .ToListAsync();

            /*var userWithPosts = await _db.Users
           .Include(user => user.Posts)
           .Where(user => user.UserId == userId)
           .SelectMany(user => user.Posts)
           .ToListAsync();*/


            return userWithPosts;
        }

        public async Task<ProfileUser> GetProfileUser([FromQuery] int userId)
        {

            var userProfile = await _db.Users
                .Include(u => u.Address)
                .Include(u => u.Card)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            var profileUser = _mapper.Map<ProfileUser>(userProfile);

            return profileUser;
        }

        public User GetUserByCard([FromQuery] string cardNumber)
        {
            var user = _db.Users.FirstOrDefault((user) => user.Card.NumberCard == cardNumber);
            if (user == null)
            {
                Console.WriteLine("not have user");
            }
            Console.WriteLine("Data " + user.LastName);
            return user;
        }

        public decimal GetAveragePostOfUser(int userId)
        {
            var posts = _db.Posts.Where((post) => post.User.UserId == userId);
            if (posts.Count() < 0)
            {
                return 0;
            }

            var data = (decimal)posts.Sum(p => p.Ratting) / posts.Count();
            return data;

        }
    }
}
