using FA.JustBlog.Common;

namespace FA.JustBlog.Entities
{
    public class Category : BaseEntity
    {



        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Slug { get; set; }

        public virtual List<Post>? Posts { get; set; }




    }
}
