using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RelationShip.Dto;
using RelationShip.Interfaces;
using RelationShip.Model;
using Microsoft.AspNetCore.Authorization;

namespace RelationShip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICaregoryReposirory _caregoryReposirory;
        private readonly IMapper _mapper;

        public CategoryController(ICaregoryReposirory categoryRepository, IMapper mapper)
        {
            _caregoryReposirory = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [Route("GetPost")]
        public IActionResult GetPostsByCategoryId([FromQuery] string category)
        {
            var posts = _caregoryReposirory.GetPostsByCategoryId(category);
            return Ok(posts);
        }

        [HttpGet]
        public IActionResult GetAllCategory()
        {
            var categories = _caregoryReposirory.GetAllCategory();
            return Ok(categories);
        }
        
        
        [HttpGet]
        [Route("Test")]
        public IActionResult Test()
        {
            return Ok("Ok");
        }


        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            _caregoryReposirory.CreateCategory(category);
            var categoryResponse = _mapper.Map<CategoryDto>(category);
            return Ok(categoryResponse);
        }

        [HttpPut]
        public IActionResult UpdateCategory(CategoryDto categoryRequest)
        {
            var category = _mapper.Map<Category>(categoryRequest);
            var response = _caregoryReposirory.UpdateCategory(category);
            var categoryResponse = _mapper.Map<CategoryDto>(response);
            return Ok(categoryResponse);
        }
    }
}
