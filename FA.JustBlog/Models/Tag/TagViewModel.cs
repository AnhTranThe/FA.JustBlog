using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Models.Tag
{
    public class TagViewModel
    {


        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateAt { get; set; }
        public string? Createby { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UpdateAt { get; set; }
        public string? Updateby { get; set; }
    }
}
