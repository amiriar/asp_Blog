using AutoMapper;
using Blog.Data;
using Blog.Data;
using Blog.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Blog.Repository.Blogs
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogDbContext context;
        private readonly IMapper mapper;

        public BlogRepository(BlogDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<Models.Blog>> GetAll(
            [FromQuery] string? filterOn = null,
            [FromQuery] string? filterQuery = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool? isAscending = true,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 25
            )
        {
            var query = context.Blogs.AsQueryable();

            // Apply filtering logic
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                filterOn = filterOn.Trim().ToLower();

                switch (filterOn)
                {
                    case "title":
                        query = query.Where(x => x.Title.Contains(filterQuery));
                        break;
                    case "description":
                        query = query.Where(x => x.Description.Contains(filterQuery));
                        break;
                    case "id":
                        if (Guid.TryParse(filterQuery, out var id))
                        {
                            query = query.Where(x => x.Id == id);
                        }
                        break;
                    default:
                        // No action for unrecognized filters
                        break;
                }
            }

            // Apply sorting logic
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = sortBy.Trim().ToLower();

                // Check if sorting order is ascending or descending
                bool isSortAscending = isAscending ?? true;

                query = sortBy switch
                {
                    "title" => isSortAscending ? query.OrderBy(x => x.Title) : query.OrderByDescending(x => x.Title),
                    "description" => isSortAscending ? query.OrderBy(x => x.Description) : query.OrderByDescending(x => x.Description),
                    _ => query // No sorting if sortBy doesn't match
                };
            }

            // Apply pagination logic
            var skipResults = page > 0 ? (page - 1) * pageSize : 0;

            return await query.Skip(skipResults).Take(pageSize).ToListAsync();
        }


        public async Task<Models.Blog?> GetById(Guid Id)
        {
            var blog = await context.Blogs.FirstOrDefaultAsync(b => b.Id == Id);
            return blog;
        }

        public async Task<Models.Blog> Create(CreateBlogdto createBlogdto)
        {
            var mappedData = mapper.Map<Models.Blog>(createBlogdto);
            var blog = await context.Blogs.AddAsync(mappedData);
            await context.SaveChangesAsync();
            return blog.Entity;
        }

        public async Task<Models.Blog> Update(Guid Id, UpdateBlogdto updateBlogdto)
        {
            var blog = await context.Blogs.FirstOrDefaultAsync(b => b.Id == Id);
            if (blog == null)
            {
                return null;
            }

            blog.Title = updateBlogdto.Title;
            blog.Description = updateBlogdto.Description;

            await context.SaveChangesAsync();
            return blog;
        }

        public async Task<Models.Blog> Delete(Guid Id)
        {
            var blog = await context.Blogs.FirstOrDefaultAsync(b => b.Id ==Id);
            context.Blogs.Remove(blog);
            await context.SaveChangesAsync();

            return blog;
        }
    }
}
