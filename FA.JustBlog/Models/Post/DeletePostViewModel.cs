using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Models.Post
{
    public class DeletePostViewModel
    {
        [Required]
        public string? PostId { get; set; }


    }
}
