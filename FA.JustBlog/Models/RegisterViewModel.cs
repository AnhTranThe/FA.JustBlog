using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Models
{
    public class RegisterViewModel
    {


        [Required]
        [Display(Name = "Firstname")]
        public required string FirstName { get; set; }

        [Required]
        [Display(Name = "Lastname")]
        public required string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-Mail")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [StringLength(100, ErrorMessage = "{0} at least {2} maximum {1} symbol length", MinimumLength = 8)]
        [RegularExpression("^(?=[A-Za-z0-9@#$%^&+!=]+$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%^&+!=])(?=.{8,}).*$", ErrorMessage = "Mật khẩu phải có từ 8 kí tự trở lên với ít nhất 1 chữ hoa, 1 số, 1 kí tự đặc biệt")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public required string Password { get; set; }
        [Required(ErrorMessage = "Please enter your confirm password")]
        [StringLength(100, ErrorMessage = "{0} at least {2} maximum {1} symbol length", MinimumLength = 8)]
        [RegularExpression("^(?=[A-Za-z0-9@#$%^&+!=]+$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%^&+!=])(?=.{8,}).*$", ErrorMessage = "Mật khẩu phải có từ 8 kí tự trở lên với ít nhất 1 chữ hoa, 1 số, 1 kí tự đặc biệt")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public required string ConfirmPassword { get; set; }

    }
}
