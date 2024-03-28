using AutoMapper;
using FA.JustBlog.Common;
using FA.JustBlog.Entities;
using FA.JustBlog.Interface;
using FA.JustBlog.Models.Category;
using FA.JustBlog.Models.Post;
using FA.JustBlog.Models.Tag;
using FA.JustBlog.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FA.JustBlog.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public PostsController(IPostRepository postRepository, ITagRepository tagRepository, IUserRepository userRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index(string tag)
        {
            IQueryable<Entities.Post> postQuery = _postRepository.Posts.Where(i => i.IsActive);

            if (!string.IsNullOrEmpty(tag))
            {
                postQuery = (
                    from a in postQuery
                    join b in _postRepository.mapPostTags on a.Id equals b.PostId
                    join c in _tagRepository.Tags on b.TagId equals c.Id
                    where tag.Contains(c.Name)
                    select a
                );
            }
            List<Entities.Post> postList = await postQuery.AsNoTracking().ToListAsync();

            return View(
                    new PostListViewModel { Posts = postList });
        }

        [Authorize(Roles = ConstantSystem.RoleUserName + "," + ConstantSystem.RoleAdminName)]

        [Route("posts/list/{userid}")]
        public async Task<IActionResult> List(string UserId)
        {
            AppRole? result = await _userRepository.GetRoleByUserId(UserId);
            IQueryable<Post> posts = _postRepository.Posts;
            if (result != null)
            {

                posts = posts.Where(i => i.UserId == UserId);

            }
            PostListViewModel postListViewModel = new()
            {
                Posts = await posts.ToListAsync()
            };
            return View(postListViewModel);
        }


        public async Task<IActionResult> Detail(string? PostId)
        {
            if (PostId == null)
            {
                return NotFound(); // Handle case where PostId is null
            }

            Post? post = await _postRepository.FindAsync(PostId);

            PostViewModel postViewModel = new();
            if (post != null)
            {
                postViewModel = _mapper.Map<PostViewModel>(post);

            }

            if (postViewModel == null)
            {
                ViewData["No Result"] = "No Result";

            }

            return View(postViewModel);
        }
        [HttpGet]
        [Authorize(Roles = ConstantSystem.RoleUserName + "," + ConstantSystem.RoleAdminName)]
        public async Task<IActionResult> Create(string? returnUrl)
        {

            IQueryable<Category> categoriesQuery = _categoryRepository.Categories;
            IQueryable<Tag> tagsQuery = _tagRepository.Tags;
            List<Category> categoryLs = new();
            List<Tag> tagLs = new();

            CreatePostViewModel model = new();

            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                model.ReturnUrl = returnUrl;

            }
            if (categoriesQuery != null)
            {
                categoryLs = await categoriesQuery.ToListAsync();
            }
            if (tagsQuery != null)
            {
                tagLs = await tagsQuery.ToListAsync();
            }

            model.categoryViewModels = _mapper.Map<List<CategoryViewModel>>(categoryLs);
            model.tagViewModels = _mapper.Map<List<TagViewModel>>(tagLs); ;

            return View(model);
        }


        [HttpPost]
        public IActionResult Create(CreatePostViewModel model)
        {
            if (ModelState.IsValid)
            {
                string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                byte[] fileBytes = new byte[0];
                if (model.File is not null)
                {
                    fileBytes = CommonUtils.ConvertFileToByteArray(model.File);

                }
                _ = _postRepository.CreatePostAsync(
                    new CreatePostViewModel
                    {
                        Title = model.Title,
                        Content = model.Content,
                        Slug = CommonUtils.UrlFriendly(model.Title ?? ""),
                        UserId = userId ?? "",
                        Image = fileBytes,
                        IsActive = model.IsActive,
                        CategoryId = model.CategoryId,
                        TagIds = model.TagIds
                    }
                );

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        [Authorize(Roles = ConstantSystem.RoleUserName + "," + ConstantSystem.RoleAdminName)]
        public async Task<IActionResult> Edit(string? PostId)
        {
            if (PostId == null)
            {
                return NotFound();
            }

            Post? post = await _postRepository.FindAsync(PostId);
            EditPostViewModel postViewModel = new();
            if (post != null)
            {
                postViewModel = _mapper.Map<EditPostViewModel>(post);
                postViewModel.categoryViewModels = _mapper.Map<List<CategoryViewModel>>(await _categoryRepository.Categories.ToListAsync());
                postViewModel.tagViewModels = _mapper.Map<List<TagViewModel>>(await _tagRepository.Tags.ToListAsync());

            }
            return post == null
                ? NotFound()
                : View(postViewModel);
        }
    }
}






