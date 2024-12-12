using Blog.Dtos;
using Blog.Interface;
using Blog.Repository.Blogs;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Services
{
    public class BlogService : IBlogInterface
    {
        private readonly IBlogRepository blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            this.blogRepository = blogRepository;
        }
        public async Task<List<Models.Blog>> GetAll(string? filterOn,
            string? filterQuery,
            string? sortBy,
            bool? isAscending,
            int page = 1,
            int pageSize = 25)
        {
            return await blogRepository.GetAll(filterOn,
        filterQuery,
        sortBy,
        isAscending,
        page,
        pageSize);
        }

        public async Task<Models.Blog?> GetById(Guid Id)
        {
            var blog = await blogRepository.GetById(Id);
            if (blog == null)
            {
                return null;
            }
            return blog;
        }

        public async Task<Models.Blog> Create(CreateBlogdto createBlogdto)
        {
            return await blogRepository.Create(createBlogdto);
        }

        public async Task<Models.Blog> Update(Guid Id, UpdateBlogdto updateBlogdto)
        {
            return await blogRepository.Update(Id, updateBlogdto);
        }

        public Task<Models.Blog> Delete(Guid Id)
        {
            return blogRepository.Delete(Id);
        }
    }
}
