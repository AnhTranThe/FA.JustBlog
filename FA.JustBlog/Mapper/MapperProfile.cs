using AutoMapper;
using FA.JustBlog.Entities;
using FA.JustBlog.Models.Category;
using FA.JustBlog.Models.Comment;
using FA.JustBlog.Models.Post;
using FA.JustBlog.Models.Tag;
using FA.JustBlog.Models.User;

namespace FA.JustBlog.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            _ = CreateMap<AppUser, UserViewModel>(); ;

            _ = CreateMap<Post, PostViewModel>()
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments != null ? src.Comments.ToList() : new List<Comment>()))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.mapPostTags != null ? src.mapPostTags.Select(mpt => mpt.Tag).ToList() : new List<Tag>()))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category ?? new Category()))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User ?? new AppUser()))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image != null && src.Image.Length > 0 ? src.Image : null))
                .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => src.CreateAt.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.UpdateAt, opt => opt.MapFrom(src => src.UpdateAt.ToString("dd/MM/yyyy")));

            _ = CreateMap<Post, EditPostViewModel>()
              .ForMember(dest => dest.TagIds, opt => opt.MapFrom(src => src.mapPostTags != null ? src.mapPostTags.Select(mpt => mpt.Tag.Id).ToList() : new List<string>()))
              ;



            _ = CreateMap<EditPostViewModel, Post>()
              .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image != null && src.Image.Length > 0 ? src.Image : null));



            _ = CreateMap<CreatePostViewModel, Post>();


            _ = CreateMap<Comment, CommentViewModel>()
                .ForMember(dest => dest.CreateAt, opt => opt.MapFrom(src => src.CreateAt.ToString("dd/MM/yyyy")));


            _ = CreateMap<Tag, TagViewModel>();
            _ = CreateMap<Category, CategoryViewModel>();


        }

    }
}
