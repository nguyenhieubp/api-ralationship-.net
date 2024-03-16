using RelationShip.Dto;
using RelationShip.Model;
using RelationShip.Response.Post;

namespace RelationShip.Interfaces
{
    public interface IPostRepository
    {
        Post CreateNewPost(Post post);
        ICollection<PostWithUserDto> GetAllPosts();
        PostWithUserDto GetPostWidthUserById(int PostId);
        Post GetPostById(int PostId);
        bool ExitPost(int PostId);
        Post UpdatePost(Post post);
        Post UpdateTitlePost(Post post);
        bool DeletePost(Post post);
       
    }
}
