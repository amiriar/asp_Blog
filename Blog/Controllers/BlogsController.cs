using Microsoft.AspNetCore.Mvc;
using Blog.Interface;
using Blog.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogInterface _blogInterface;

        public BlogsController(IBlogInterface blogInterface)
        {
            _blogInterface = blogInterface;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? filterOn,
            [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy,
            [FromQuery] bool? isAscending,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 25)
        {
            var blogs = await _blogInterface.GetAll(filterOn, filterQuery, sortBy, isAscending, page, pageSize);
            return Ok(blogs);
        }

        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var blog = await _blogInterface.GetById(Id);
            if (blog == null)
            {
                return NotFound();
            }
            return Ok(blog);
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create(CreateBlogdto createBlogdto)
        {
            var blog = await _blogInterface.Create(createBlogdto);
            return Ok(blog);
        }

        [HttpPatch]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update(Guid Id, UpdateBlogdto updateBlogdto)
        {
            var UpdatedBlog = await _blogInterface.Update(Id, updateBlogdto);
            return Ok(UpdatedBlog);
        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var deletedBlog = await _blogInterface.Delete(Id);
            return Ok(deletedBlog);
        }
    }
}
