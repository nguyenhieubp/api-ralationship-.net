using Microsoft.AspNetCore.Mvc;
using RelationShip.Dto;
using RelationShip.Model;
using RelationShip.Response.Post;

namespace RelationShip.Interfaces
{
    public interface ICaregoryReposirory
    {
        Category CreateCategory(Category category);
        CategoryPost CreateCategoryPost(int PostId, int CategoryId);
        ICollection<PostWithUserDto> GetPostsByCategoryId(string category);
        Category UpdateCategory(Category category);
        ICollection<CategoryDto> GetAllCategory();
        bool ExitCategory(int categoryId);
    }
}
