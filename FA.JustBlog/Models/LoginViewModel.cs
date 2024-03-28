using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-Mail")]
        public required string Email { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Maximum ten characters!")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }




    }
}
