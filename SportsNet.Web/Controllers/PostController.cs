namespace SportsNet.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using SportsNet.Data.Models;
    using SportsNet.Data.Repositories.Interfaces;
    using SportsNet.Services.Data.Interfaces;
    using SportsNet.Services.Data.Models.Post;
    using SportsNet.Web.Infrastructure.Extensions;
    using SportsNet.Web.ViewModels.Post;
    using Category = Data.Models.Category;

    [Authorize]
    public class PostController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IPostService postService;
        private readonly IRepository<Category> categoryRepository;

        public PostController(ICategoryService categoryService, IPostService postService, IRepository<Category> categoryRepository)
        {
            this.categoryService = categoryService;
            this.postService = postService;
            this.categoryRepository = categoryRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery]AllPostsQueryModel queryModel)
        {
            AllPostsQueryServiceModel serviceModel = await this.postService.AllAsync(queryModel);

            queryModel.Posts = serviceModel.Posts;
            queryModel.TotalPosts = serviceModel.TotalPostsCount;
            queryModel.Categories = await this.categoryService.AllCategoryNamesAsync();

            return this.View(queryModel);
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

        [HttpPost]
        public async Task<IActionResult> Add(PostFormModel model)
        {
            bool categoryExists = this.categoryRepository.All().Any(c => c.Id == model.CategoryId);
            if (!categoryExists) 
            {
                this.ModelState.AddModelError(nameof(model.CategoryId), "Selected category does not exist!");
            }

            if (!this.ModelState.IsValid)
            {
                model.Categories = await this.categoryService.AllCategoriesAsync();
                model.Types = this.postService.GetPostTypes();

                return View(model);
            }

            try
            {
                await this.postService.CreateAsync(model, this.User.GetId()!);

                return RedirectToAction("All", "Post");
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new post! Please try again later or contact administrator!");
                model.Categories = await this.categoryService.AllCategoriesAsync();
                model.Types = this.postService.GetPostTypes();

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            bool postExists = await this.postService
                .ExistsByIdAsync(id);
            if (!postExists) 
            {
				return RedirectToAction("All", "Post");
			}

            try
            {
                PostDetailsViewModel post = this.postService.GetPost<PostDetailsViewModel>(id);

                return View(post);
            }
            catch (Exception)
            {

                throw;
            }
        }
	}
}
