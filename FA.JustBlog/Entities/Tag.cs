using FA.JustBlog.Common;

namespace FA.JustBlog.Entities
{
    public class Tag : BaseEntity
    {

        public virtual List<MapPostTag>? mapPostTags { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }

    }
}
