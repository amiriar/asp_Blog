namespace Blog.Dtos
{
    public class CreateBlogdto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public Guid Author { get; set; }
    }
}
