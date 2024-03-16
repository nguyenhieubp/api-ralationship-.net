using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RelationShip.Data;
using RelationShip.Dto;
using RelationShip.Interfaces;
using RelationShip.Model;
using RelationShip.Response.Post;

namespace RelationShip.Repository
{
    public class CategoryRepository : ICaregoryReposirory
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper; 

        public CategoryRepository(ApplicationDbContext db,IMapper mapper)
        {
            _db = db;            
            _mapper = mapper;
        }

        public Category CreateCategory(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
            return category;
        }

        public ICollection<CategoryDto> GetAllCategory()
        {
            var categories = _db.Categories.ToList();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
            return categoriesDto;
        }

        public bool ExitCategory(int categoryId)
        {
            return _db.Categories.Any((category) => category.CategoryId == categoryId);
        }

        public ICollection<PostWithUserDto> GetPostsByCategoryId(string category)
        {
            var result = _db.Posts
            .Include(post => post.User) // Include thông tin người dùng
            /*.Include(post => post.CategoryPosts)*/ // Include thông tin liên kết giữa bài đăng và danh mục
            /*.ThenInclude(categoryPost => categoryPost.Categories) // Include thông tin về danh mục*/
            .Where(post => post.CategoryPosts.Any(categoryPost => categoryPost.Categories != null && categoryPost.Categories.CategoryName.ToLower() == category.ToLower()))
            .ToList();


            var postDtoList = _mapper.Map<List<PostWithUserDto>>(result);
            return postDtoList;
        }


        public CategoryPost CreateCategoryPost(int PostId, int CategoryId)
        {
            var categoryPost = new CategoryPost
            {
                CategoryId = CategoryId,
                PostId = PostId,
            };
            _db.CategoryPosts.Add(categoryPost);
            _db.SaveChanges();
            return categoryPost;
        }

        public Category UpdateCategory(Category category)
        {
            _db.Categories.Update(category);
            _db.SaveChanges(true);
            return category;
        }
    }
}
