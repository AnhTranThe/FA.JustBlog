using FA.JustBlog.Entities;
using FA.JustBlog.Models.Tag;

namespace FA.JustBlog.Interface
{
    public interface ITagRepository
    {

        IQueryable<Tag> Tags { get; }
        IQueryable<MapPostTag> mapPostTags { get; }
        Task CreateTagAsync(TagViewModel tag);
        Task EditTagAsync(EditTagViewModel tag);
        Task<Tag?> FindAsync(string id);
        Task<bool> DeleteAsync(string id);
    }
}
