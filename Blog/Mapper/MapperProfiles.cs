using AutoMapper;
using Blog.Dtos;
using Blog.Models;

namespace Blog.Mapper
{
    public class AutomapperProfies : Profile
    {
        public AutomapperProfies()
        {
            CreateMap<Models.Blog, CreateBlogdto>().ReverseMap();
        }
    }
}
