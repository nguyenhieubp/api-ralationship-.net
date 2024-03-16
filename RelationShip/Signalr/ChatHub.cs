using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using RelationShip.Data;
using RelationShip.Dto;
using RelationShip.Interfaces;
using RelationShip.Model;

namespace RelationShip.Signalr
{
    public class ChatHub:Hub
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ChatHub(ApplicationDbContext db, IUserRepository userRepository, IMapper mapper)
        {
            _db = db;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task SendMessage(MessageDto messages)
        {
            User user = await _userRepository.GetUserById(messages.UserId);
            var messageModel = _mapper.Map<Messages>(messages);
            _db.Messages.Add(messageModel);
            await _db.SaveChangesAsync();
            await Clients.All.SendAsync("ReceiveMessage",user.FirstName, messages.Message);
        }

    }
}
