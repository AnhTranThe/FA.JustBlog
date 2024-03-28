using FA.JustBlog.Entities;
using FA.JustBlog.Models.Category;

namespace FA.JustBlog.Interface
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }
        Task CreateCategoryAsync(CreateCategoryViewModel category);
        Task EditCategoryAsync(EditCategoryViewModel category);
        Task<Category?> FindAsync(string id);
        Task<bool> DeleteAsync(string id);
    }
}
