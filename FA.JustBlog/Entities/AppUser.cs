using Microsoft.AspNetCore.Identity;

namespace FA.JustBlog.Entities
{
    public class AppUser : IdentityUser<string>
    {





        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        private string _FullName = string.Empty;
        public string FullName
        {
            get => _FullName;
            set
            {
                _FullName = value;
                _FullName = LastName + " " + FirstName;
            }


        }
        public byte[]? Image { get; set; }

        public bool IsActive { get; set; } = true;

        public virtual List<Post>? Posts { get; set; }
        public virtual List<Comment>? Comments { get; set; }






    }
}
