using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RelationShip.Data;
using RelationShip.Interfaces;
using RelationShip.Model;
using RelationShip.Response.Post;

namespace RelationShip.Repository
{
    public class PostRepository: IPostRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper; 
        public PostRepository(ApplicationDbContext db,IMapper mapper)
        {
            _db = db;            
            _mapper = mapper;
        }


        public Post CreateNewPost([FromBody] Post post)
        {
            _db.Posts.Add(post);
            _db.SaveChanges();
            return post;
        }

        public bool ExitPost(int PostId)
        {
            return _db.Posts.Any((post) => post.PostId == PostId);
        }

        public ICollection<PostWithUserDto> GetAllPosts()
        {
            var posts = _db.Posts
                        .Include(post => post.User)
                        .OrderBy(post => post.PostId)
                        .Where((post) => !post.Title.Contains("i"))
                        .ToList();
            var postDtoList = _mapper.Map<List<PostWithUserDto>>(posts);
            return postDtoList;
        }


        public Post GetPostById(int PostId)
        {
            return _db.Posts.FirstOrDefault((post) => post.PostId == PostId);
        }

        public Post UpdatePost(Post post)
        {
            _db.Posts.Update(post);
            _db.SaveChanges(true);
            return post;
        }

        public Post UpdateTitlePost(Post post)
        {
            _db.Posts.Update(post);
            _db.SaveChanges(true);
            return post;
        }

        public bool DeletePost(Post post)
        {
            _db.Posts.Remove(post);
            _db.SaveChanges();
            return true;
        }

        public PostWithUserDto GetPostWidthUserById(int PostId)
        {
            var post = _db.Posts.Include((post) => post.User).FirstOrDefault((post) => post.PostId == PostId);
            var postWithUser =_mapper.Map<PostWithUserDto>(post);
            return postWithUser;
        }


     
    }
}
