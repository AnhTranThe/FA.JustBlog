using FA.JustBlog.Models.Post;
using FA.JustBlog.Models.Tag;

namespace FA.JustBlog.Models.MapPostTag
{
    public class MapPostTagViewModel
    {
        public string? PostId { get; set; }
        public string? TagId { get; set; }
        public PostViewModel Post { get; set; } = null!;
        public TagViewModel Tag { get; set; } = null!;
    }
}
