using RelationShip.Dto;

namespace RelationShip.Response.Post
{
    public class PostWithUserDto
    {
        public int PostId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public double Ratting {  get; set; }    

        public UserDto User { get; set; }
    }
}
