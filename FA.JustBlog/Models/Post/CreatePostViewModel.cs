using FA.JustBlog.Models.Category;
using FA.JustBlog.Models.Tag;
using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Models.Post
{
    public class CreatePostViewModel
    {
        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Content { get; set; }

        public bool IsActive { get; set; } = true;

        public IFormFile? File { get; set; }

        public byte[]? Image { get; set; }

        public string? Slug { get; set; }

        public List<CategoryViewModel> categoryViewModels { get; set; } = new List<CategoryViewModel>();

        public List<TagViewModel> tagViewModels { get; set; } = new List<TagViewModel>();
        [Required(ErrorMessage = "Please select a category")]
        public string? CategoryId { get; set; }
        [Required(ErrorMessage = "Please select a tag or more")]
        public List<string>? TagIds { get; set; }

        public string? UserId { get; set; }

        public string? ReturnUrl { get; set; }


    }
}
