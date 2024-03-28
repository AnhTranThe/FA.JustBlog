using FA.JustBlog.Common;

namespace FA.JustBlog.Entities
{
    public class Post : BaseEntity
    {

        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Slug { get; set; }
        public byte[]? Image { get; set; } = null;
        public string? ImageAlt { get; set; }
        public int CountOfView { get; set; } = 0;
        public bool IsActive { get; set; } = true;

        public string? UserId { get; set; }
        public virtual AppUser? User { get; set; }

        public virtual List<MapPostTag>? mapPostTags { get; set; }
        public string? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual List<Comment>? Comments { get; set; }


    }
}
