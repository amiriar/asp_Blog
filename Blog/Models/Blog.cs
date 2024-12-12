namespace Blog.Models
{
    public class Blog
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; }
        public Guid Author { get; set; }
    }
}
