using FA.JustBlog.Models.Post;

namespace FA.JustBlog.Models.Category
{
    public class CategoryViewModel
    {

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Slug { get; set; }

        public List<PostViewModel>? Posts { get; set; }
    }
}
