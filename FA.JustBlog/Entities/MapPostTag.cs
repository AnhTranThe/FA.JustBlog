namespace FA.JustBlog.Entities
{
    public class MapPostTag
    {
        public required string PostId { get; set; }
        public required string TagId { get; set; }
        public virtual Post Post { get; set; } = null!;
        public virtual Tag Tag { get; set; } = null!;
    }
}
