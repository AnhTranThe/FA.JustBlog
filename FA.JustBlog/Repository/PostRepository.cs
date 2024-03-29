using FA.JustBlog.Data;
using FA.JustBlog.Entities;
using FA.JustBlog.Interface;
using FA.JustBlog.Models.Post;
using FA.JustBlog.Utilities;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILoggingRepository _loggingRepository;

        public PostRepository(ApplicationDbContext context, ILoggingRepository loggingRepository)
        {
            _context = context;
            _loggingRepository = loggingRepository;

        }
        public IQueryable<Post> Posts => _context.Posts;

        public IQueryable<MapPostTag> mapPostTags => _context.MapPostTags;

        public async Task CreatePostAsync(CreatePostViewModel post)
        {

            try
            {
                if (!string.IsNullOrWhiteSpace(post.UserId) && !string.IsNullOrWhiteSpace(post.CategoryId))
                {
                    Post newPost = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Title = post.Title,
                        Slug = post.Slug,
                        Content = post.Content,
                        Image = post.Image,
                        IsActive = post.IsActive,
                        CategoryId = post.CategoryId,
                        UserId = post.UserId,
                        mapPostTags = new List<MapPostTag>()

                    };

                    //User = await _context.Users.FindAsync(post.UserId), // Initialize User
                    //    mapPostTags = new List<MapPostTag>(), // Initialize mapPostTags       
                    if (post.TagIds != null && post.TagIds.Count > 0)
                    {
                        foreach (string tagId in post.TagIds)
                        {
                            newPost.mapPostTags.Add(new MapPostTag { PostId = newPost.Id, TagId = tagId });
                        }

                    }

                    _ = _context.Posts.Add(newPost);
                    _ = await _context.SaveChangesAsync(newPost.UserId);


                }

            }
            catch (Exception ex)
            {
                _loggingRepository.LogError(ex);
            }

        }

        public async Task<Post?> FindAsync(string id)
        {

            return await Posts
            .Include(p => p!.User!) // Bao gồm thông tin người dùng liên quan
            .Include(p => p!.mapPostTags!).ThenInclude(mpt => mpt.Tag) // Bao gồm các thẻ liên quan
            .Include(p => p!.Category!) // Bao gồm thông tin về danh mục
            .Include(p => p!.Comments!).ThenInclude(c => c.User)// Bao gồm bình luận và thông tin người dùng của chúng
            .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);

        }

        public async Task<bool> DeleteAsync(string id)
        {
            Post? post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                //remove mapPostTags before remove post
                IQueryable<MapPostTag> mapPostTags = _context.MapPostTags.Where(mpt => mpt.PostId == id);
                _context.MapPostTags.RemoveRange(mapPostTags);
                _ = _context.Posts.Remove(post);
                _ = await _context.SaveChangesAsync();
                return true; // Return true to indicate successful deletion
            }
            return false; // Return false if the post with the provided ID does not exist
        }

        public async Task EditPostAsync(EditPostViewModel post, string UserId)
        {
            try
            {
                // Find the existing post by its ID

                Post? existingPost = await FindAsync(post.PostId ?? "");
                if (existingPost == null)
                {
                    throw new InvalidOperationException("Post not found.");
                }

                // Update the properties of the existing post with the new values
                existingPost.Title = post.Title;
                existingPost.Content = post.Content;
                existingPost.Slug = !string.IsNullOrWhiteSpace(post.Slug) ? post.Slug : CommonUtils.UrlFriendly(post.Title ?? "");
                if (post.Image != null && post.Image.Length > 0)
                {
                    existingPost.Image = post.Image;
                }

                existingPost.IsActive = post.IsActive;
                existingPost.CategoryId = post.CategoryId;

                // Clear existing tags and add new ones
                if (existingPost.mapPostTags != null && post.TagIds != null && post.PostId != null && existingPost.mapPostTags.Select(t => t.Tag.Id).ToList() != post.TagIds)
                {
                    existingPost.mapPostTags.Clear();
                    foreach (string tagId in post.TagIds)
                    {
                        existingPost.mapPostTags.Add(new MapPostTag { PostId = post.PostId, TagId = tagId });
                    }

                }

                // Save the changes to the database
                _ = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _loggingRepository.LogError(ex);
                throw;
            }


        }
    }
}



