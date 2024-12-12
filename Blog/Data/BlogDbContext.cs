using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

        public DbSet<Models.Blog> Blogs { get; set; } 
    }
}
