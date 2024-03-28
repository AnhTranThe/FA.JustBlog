using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Models.Category
{
    public class DeleteCategoryViewModel
    {
        [Required]
        public string? CategoryId { get; set; }
    }
}
