using FA.JustBlog.Data;
using FA.JustBlog.Entities;
using FA.JustBlog.Interface;
using FA.JustBlog.Models.Category;

namespace FA.JustBlog.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;

        }

        public IQueryable<Category> Categories => _context.Categories;

        public Task CreateCategoryAsync(CreateCategoryViewModel category)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task EditCategoryAsync(EditCategoryViewModel category)
        {
            throw new NotImplementedException();
        }

        public Task<Category?> FindAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
