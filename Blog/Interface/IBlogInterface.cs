using Blog.Dtos;
using Blog.Models;

namespace Blog.Interface
{
    public interface IBlogInterface
    {
        Task<List<Blog.Models.Blog>> GetAll(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool? isAscending = true, int page = 1, int pageSize = 25);
        Task<Models.Blog?> GetById(Guid Id);
        Task<Models.Blog> Create(CreateBlogdto createBlogdto);
        Task<Models.Blog> Update(Guid Id, UpdateBlogdto updateBlogdto);
        Task<Models.Blog> Delete(Guid Id);
    }
}
