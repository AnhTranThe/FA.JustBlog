using FA.JustBlog.Models.Post;
using FA.JustBlog.Models.User;

namespace FA.JustBlog.Models.Comment
{
    public class CommentViewModel
    {
        public string Id { get; set; } = null!;
        public string? Text { get; set; }
        public string? CreateAt { get; set; }
        public string? CreateBy { get; set; }
        public string? PostId { get; set; }
        public PostViewModel Post { get; set; } = null!;
        public string? UserId { get; set; }
        public UserViewModel User { get; set; } = null!;

    }
}
