using AutoMapper;
using RelationShip.Dto;
using RelationShip.Model;
using RelationShip.RequestBody.Post;
using RelationShip.RequestBody.User;
using RelationShip.Response.Message;
using RelationShip.Response.Post;
using RelationShip.Response.User;

namespace RelationShip.Helper
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            //user
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<NewUserRequest,User>();
            CreateMap<User, ProfileUser>();
            CreateMap<Address, AddressDto>();
            CreateMap<Card, CardDto>();

            //category
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryDto, Category>();
            
            //message
            CreateMap<Messages,MessageDto>().ReverseMap();
            CreateMap<MessageResponse,Messages>().ReverseMap();

            //auth
            CreateMap<Auth,AuthDto>().ReverseMap();


            //OTP
            CreateMap<OTP, OtpDto>().ReverseMap();

            //post
            CreateMap<Post, PostDto>();
            CreateMap<Post, PostWithUserDto>();
            CreateMap<RequestBodyPost, Post>();
            CreateMap<RequestBodyUpdatePost, Post>();
        }
    }
}
