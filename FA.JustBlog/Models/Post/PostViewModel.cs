using FA.JustBlog.Models.Category;
using FA.JustBlog.Models.Comment;
using FA.JustBlog.Models.Tag;
using FA.JustBlog.Models.User;
using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Models.Post
{
    public class PostViewModel
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Slug { get; set; }
        public byte[]? Image { get; set; }
        public string? ImageAlt { get; set; }

        public bool IsActive { get; set; } = true;


        [Required]
        public string? UserId { get; set; }
        public UserViewModel User { get; set; } = new UserViewModel();

        public string? CategoryId { get; set; }
        public CategoryViewModel Category { get; set; } = new CategoryViewModel();
        public List<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();
        public List<TagViewModel> Tags { get; set; } = new List<TagViewModel>();

        public string? CreateAt { get; set; }
        public string? Createby { get; set; }
        public string? UpdateAt { get; set; }
        public string? Updateby { get; set; }


    }
}
