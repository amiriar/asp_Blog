namespace Blog.Dtos
{
    public class UpdateBlogdto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}
