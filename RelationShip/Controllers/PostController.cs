using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RelationShip.Dto;
using RelationShip.Interfaces;
using RelationShip.Model;
using RelationShip.RequestBody.Post;

namespace RelationShip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly ICaregoryReposirory _caregoryReposirory;

        public PostController(IPostRepository postRepository,ICaregoryReposirory caregoryReposirory ,IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _caregoryReposirory = caregoryReposirory;
        }

        [HttpGet]
/*        [LogFilter(name: "siuu")]*/
        [ProducesResponseType<IEnumerable<Post>>(StatusCodes.Status200OK)]
        public IActionResult GetAllPost()
        {
            var posts = _postRepository.GetAllPosts();
            return Ok(posts);
        }

        [HttpGet("{PostId:int}")]
        [ProducesResponseType<IEnumerable<Post>>(StatusCodes.Status200OK)]
        public IActionResult GetPostById([FromRoute] int PostId)
        {
            if (!_postRepository.ExitPost(PostId))
                return NotFound();

            var post = _postRepository.GetPostWidthUserById(PostId);
           
            // {user: ahsh,post: []}
            return Ok(post);
        }

        [HttpPost]
        public IActionResult CreateNewPost([FromBody] RequestBodyPost newPost,[FromQuery] int CategoryId)
        {
            if (!_caregoryReposirory.ExitCategory(CategoryId))
            {
                return BadRequest("Category does not exist.");
            }
            var post = _mapper.Map<Post>(newPost);
            _postRepository.CreateNewPost(post);
            var postDto = _mapper.Map<PostDto>(post);
            _caregoryReposirory.CreateCategoryPost(postDto.PostId, CategoryId);
            return Ok(postDto);
        }

        [HttpPut]
        [Route("{PostId:int}")]
        public IActionResult UpdatePost([FromBody] RequestBodyUpdatePost dataPostUpdate, [FromRoute] int PostId)
        {
            var postUpdate = _postRepository.GetPostById(PostId);

            if (postUpdate == null)
            {
                return NotFound($"Post with id {PostId} not found");
            }

            // Sử dụng Automapper để ánh xạ thuộc tính từ dataPostUpdate vào postUpdate
            _mapper.Map(dataPostUpdate, postUpdate);

            var post = _mapper.Map<PostDto>(_postRepository.UpdatePost(postUpdate));

            return Ok(post);
        }

        [HttpPatch]
        [Route("Title/{PostId:int}")]
        public IActionResult UpdateTitlePost([FromRoute] int PostId, [FromBody] string title)
        {
            var postUpdate = _postRepository.GetPostById(PostId);

            if (postUpdate == null)
            {
                return NotFound($"Post with id {PostId} not found");
            }

            postUpdate.Title = title;
            var post = _mapper.Map<PostDto>(_postRepository.UpdateTitlePost(postUpdate));
            return Ok(post);
        }

        [HttpDelete]
        [Route("{PostId:int}")]
        public IActionResult DeletePost([FromRoute] int PostId)
        {
            var postDelete = _postRepository.GetPostById(PostId);
            if (postDelete == null)
            {
                return NotFound($"Post with id {PostId} not found");
            }
            bool post = _postRepository.DeletePost(postDelete);
            return Ok(post);
        }

    }
}
