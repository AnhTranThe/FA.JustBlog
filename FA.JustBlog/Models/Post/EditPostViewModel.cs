
using FA.JustBlog.Models.Category;
using FA.JustBlog.Models.Tag;
using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Models.Post
{
    public class EditPostViewModel
    {

        [Required]
        public string? PostId { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Content { get; set; }

        public bool IsActive { get; set; }



        public byte[]? Image { get; set; }


        public List<CategoryViewModel> categoryViewModels { get; set; } = new List<CategoryViewModel>();

        public List<TagViewModel> tagViewModels { get; set; } = new List<TagViewModel>();


        [Required]
        public string? CategoryId { get; set; }


        //public Entities.Category? Category { get; set; }
        [Required]
        public List<string>? TagIds { get; set; }

        public string? Slug { get; set; }


        public string? ReturnUrl { get; set; }







    }
}
