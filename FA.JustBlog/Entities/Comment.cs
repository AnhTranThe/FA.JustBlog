using FA.JustBlog.Common;

namespace FA.JustBlog.Entities
{
    public class Comment : BaseEntity
    {


        public string? Text { get; set; }
        public DateTime PublishedOn { get; set; }
        public string? PostId { get; set; }
        public virtual Post? Post { get; set; }
        public string? UserId { get; set; }
        public virtual AppUser? User { get; set; }

    }
}
