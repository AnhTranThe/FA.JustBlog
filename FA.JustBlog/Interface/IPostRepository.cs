using FA.JustBlog.Entities;
using FA.JustBlog.Models.Post;

namespace FA.JustBlog.Interface
{
    public interface IPostRepository
    {
        IQueryable<Post> Posts { get; }
        IQueryable<MapPostTag> mapPostTags { get; }
        Task CreatePostAsync(CreatePostViewModel post);
        Task EditPostAsync(EditPostViewModel post);
        Task<Post?> FindAsync(string id);
        Task<bool> DeleteAsync(string id);


    }
}
