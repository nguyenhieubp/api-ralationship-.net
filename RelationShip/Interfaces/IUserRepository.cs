using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RelationShip.Dto;
using RelationShip.Model;
using RelationShip.Response.User;

namespace RelationShip.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserById(int userID);
        IEnumerable<User> GetUsers();
        bool UserExists(int userId);
        Task<IEnumerable<User>> RattingUSerDESC();
        Task<IEnumerable<User>> RattingUSerASC();
        User CreateUser(User user);
        User UpdateUser(User user,int UserId);
        Task<IEnumerable<Post>> GetAllPostOfUser(int UserId);
        bool DeleteUser(int UserId);
        Task<ProfileUser> GetProfileUser([FromQuery] int userId);
        User GetUserByCard([FromQuery] string cardNumber);
        decimal GetAveragePostOfUser(int userId);
    }
}
