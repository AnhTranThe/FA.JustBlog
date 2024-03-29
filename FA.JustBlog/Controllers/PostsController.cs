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
        private readonly ILoggingRepository _logging;
        private readonly IMapper _mapper;


        public PostsController(IPostRepository postRepository, ITagRepository tagRepository, IUserRepository userRepository, ICategoryRepository categoryRepository, IMapper mapper, ILoggingRepository logging)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logging = logging;
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
        public async Task<IActionResult> Create(CreatePostViewModel model)
        {
            if (ModelState.IsValid)
            {
                string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                byte[] fileBytes = new byte[0];
                if (model.File is not null)
                {
                    fileBytes = CommonUtils.ConvertFileToByteArray(model.File);

                }
                await _postRepository.CreatePostAsync(
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

            List<Category> categoryLs = await _categoryRepository.Categories.ToListAsync(); ;
            List<Tag> tagLs = await _tagRepository.Tags.ToListAsync();

            CreatePostViewModel createPostViewModel = new()
            {
                categoryViewModels = _mapper.Map<List<CategoryViewModel>>(categoryLs),
                tagViewModels = _mapper.Map<List<TagViewModel>>(tagLs)
            };
            ;

            return View(createPostViewModel);
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

        [HttpPost]
        [Authorize(Roles = ConstantSystem.RoleUserName + "," + ConstantSystem.RoleAdminName)]
        public async Task<IActionResult> Edit(EditPostViewModel model, IFormFile? ImageFile)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        // Read the image data from the uploaded file
                        model.Image = CommonUtils.ConvertFileToByteArray(ImageFile);
                    }

                    string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    // Update the post using the repository method
                    await _postRepository.EditPostAsync(model, userId ?? "");

                    // Redirect to a success page or return a success response
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Log the error
                    _logging.LogError(ex);
                    List<Category> categoryLs = await _categoryRepository.Categories.ToListAsync(); ;
                    List<Tag> tagLs = await _tagRepository.Tags.ToListAsync();
                    model.categoryViewModels = _mapper.Map<List<CategoryViewModel>>(categoryLs);
                    model.tagViewModels = _mapper.Map<List<TagViewModel>>(tagLs);
                    ViewData["ErrorMessage"] = ex.Message;


                    return View(model);

                    // Return an error view or a generic error message

                }
            }
            else
            {
                List<Category> categoryLs = await _categoryRepository.Categories.ToListAsync(); ;
                List<Tag> tagLs = await _tagRepository.Tags.ToListAsync();
                model.categoryViewModels = _mapper.Map<List<CategoryViewModel>>(categoryLs);
                model.tagViewModels = _mapper.Map<List<TagViewModel>>(tagLs);
                ViewData["ErrorMessage"] = "Model validation failed. Please correct the errors and try again.";

                // If model validation fails, return the view with validation errors
                return View(model);
            }

        }


        [HttpPost, ActionName("DeletePermanently")]
        [Authorize(Roles = ConstantSystem.RoleAdminName)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePermanently(string PostId)
        {
            try
            {
                // Check if postId is valid
                if (string.IsNullOrEmpty(PostId))
                {
                    return BadRequest("Invalid post ID");
                }

                // Find the post in the database by PostId
                Post? postToDelete = await _postRepository.FindAsync(PostId);


                // Check if the post exists
                if (postToDelete == null)
                {
                    return NotFound("Post not found");
                }

                // Remove the post from the database


                _ = await _postRepository.DeleteAsync(postToDelete.Id);

                string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                return RedirectToAction("list", new { UserId = userId });

            }
            catch (Exception ex)
            {
                // Log the exception
                _logging.LogError(ex);

                // Optionally, return an error view or message
                return StatusCode(500, "An error occurred while deleting the post");
            }


        }

    }
}






