namespace SportsNet.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SportsNet.Services.Data.Interfaces;
    using SportsNet.Web.ViewModels.Post;

    [Authorize]
    public class PostController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IPostService postService;

        public PostController(ICategoryService categoryService, IPostService postService)
        {
            this.categoryService = categoryService;
            this.postService = postService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                PostFormModel model = new PostFormModel()
                {
                    Categories = await this.categoryService.AllCategoriesAsync(),
                    Types = this.postService.GetPostTypes()
                };

                return View(model);
            }
            catch (Exception e )
            {
                await Console.Out.WriteLineAsync(e.Message);
                throw;
            }
            
        }
    }
}
