using FA.JustBlog.Data;
using FA.JustBlog.Entities;
using FA.JustBlog.Interface;
using FA.JustBlog.Models.Tag;

namespace FA.JustBlog.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;
        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<Tag> Tags => _context.Tags;

        public IQueryable<MapPostTag> mapPostTags => _context.MapPostTags;

        public Task CreateTagAsync(TagViewModel tag)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task EditTagAsync(EditTagViewModel tag)
        {
            throw new NotImplementedException();
        }

        public Task<Tag?> FindAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
