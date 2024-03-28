using FA.JustBlog.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.ViewComponents
{
    public class TagsMenu : ViewComponent
    {
        private readonly ITagRepository _tagRepository;
        public TagsMenu(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _tagRepository.Tags.ToListAsync());
        }

    }
}