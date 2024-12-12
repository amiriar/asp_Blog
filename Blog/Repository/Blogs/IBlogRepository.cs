using Blog.Dtos;

namespace Blog.Repository.Blogs
{
    public interface IBlogRepository
    {
        Task<List<Models.Blog>> GetAll(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool? isAscending = true, int page = 1, int pageSize = 25);
        Task<Models.Blog?> GetById(Guid Id);
        Task<Models.Blog> Create(CreateBlogdto createBlogdto);
        Task<Models.Blog> Update(Guid Id, UpdateBlogdto updateBlogdto);
        Task<Models.Blog> Delete(Guid Id);
    }
}
