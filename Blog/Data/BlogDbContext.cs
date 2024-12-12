using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    //public class BlogDbContext : DbContext
    public class BlogDbContext : IdentityDbContext<IdentityUser>
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

        public DbSet<Models.Blog> Blogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            var readerRole = "06e39a99-e87c-44c6-a3eb-109e6ed2a35d";
            var writerRole = "f33fdc5c-01e5-441a-8749-f9b632bb14ea";

            var roles = new List<IdentityRole>{
                new IdentityRole
                {
                    Id = readerRole,
                    ConcurrencyStamp = readerRole,
                    Name = "Reader",
                    NormalizedName = "reader".ToUpper()

                },
                new IdentityRole
                {
                    Id = writerRole,
                    ConcurrencyStamp = writerRole,
                    Name = "Writer",
                    NormalizedName = "writer".ToUpper()
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }


}
