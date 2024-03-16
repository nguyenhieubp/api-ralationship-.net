using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RelationShip.Dto;
using RelationShip.Interfaces;
using RelationShip.Model;
using RelationShip.RequestBody.User;

namespace RelationShip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<UserDto>> GetUsers()
        {
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(users);
        }

        [HttpGet]
        [Route("{userId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetUserById([FromRoute] int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            var user = _mapper.Map<UserDto>(await _userRepository.GetUserById(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpGet]
        [Route("Ratting/User/ASC")]
        public async Task<ActionResult<IEnumerable<User>>> RattingUSerASC()
        {
            var users = _mapper.Map<List<UserDto>>(await _userRepository.RattingUSerASC());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }
        
        [HttpGet]
        [Route("Ratting/User/DESC")]
        public async Task<ActionResult<IEnumerable<User>>> RattingUSerDESC()
        {
            var users = _mapper.Map<List<UserDto>>(await _userRepository.RattingUSerDESC());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }

        [HttpPost]
        [Route("CreateUser")]
        public ActionResult<UserDto> CreateUser([FromBody] NewUserRequest createUser)
        {
            var userMap = _mapper.Map<User>(createUser);
            var newUser = _userRepository.CreateUser(userMap);
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newUserDto = _mapper.Map<UserDto>(newUser);
            
            return Ok(newUserDto);
        }

        [HttpPut]
        [Route("UpdateUser/{UserId:int}")]
        public ActionResult<UserDto> UpdateUser([FromBody] UserDto dataUserUpdate, [FromRoute] int UserId)
        {
            if(!_userRepository.UserExists(UserId))
                return NotFound();
            
            var userUpdate = _mapper.Map<User>(dataUserUpdate);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _userRepository.UpdateUser(userUpdate, UserId);

            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto); 
        }

        [HttpDelete]
        [Route("Delete/{UserId:int}")]
        public ActionResult<UserDto> DeleteUser([FromRoute] int UserId)
        {
            if (!_userRepository.UserExists(UserId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool result = _userRepository.DeleteUser(UserId);

            return Ok(result);
        }

        [HttpGet]
        [Route("PostOfUser/{UserId:int}")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPostOfUser([FromRoute] int UserId)
        {
            if (!_userRepository.UserExists(UserId))
                return NotFound();
            var user = await _userRepository.GetUserById(UserId);
            var posts = await _userRepository.GetAllPostOfUser(UserId);

            var userDto = _mapper.Map<UserDto>(user);
            var postsDto = _mapper.Map<List<PostDto>>(posts);

            var result = new
            {
                user = userDto, // Thay thế bằng thông tin user thực tế của bạn
                post = postsDto // Có thể thêm nhiều bài post vào danh sách nếu cần
            };

            return Ok(result);
        }

        [HttpGet]
        [Route("GetProfileUser")]
        public async Task<IActionResult> GetProfileUser([FromQuery] int userId)
        {
            var profile = await _userRepository.GetProfileUser(userId);
            return Ok(profile);
        }

        [HttpGet]
        [Route("GetUserByNumberCard")]
        public IActionResult GetUserByCard([FromQuery] string numberCard)
        {
            var user = _userRepository.GetUserByCard(numberCard);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpGet]
        [Route("Ratting/Post")]
        public IActionResult GetAveragePostOfUser([FromQuery] int userId)
        {
            decimal ratting = _userRepository.GetAveragePostOfUser(userId);
            return Ok(ratting.ToString("0.00"));
        }
    }
}
