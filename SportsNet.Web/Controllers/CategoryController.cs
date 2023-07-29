namespace SportsNet.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
	using SportsNet.Data.Models;
	using SportsNet.Data.Repositories.Interfaces;
	using SportsNet.Services.Data.Interfaces;
	using SportsNet.Services.Data.Models.Post;
	using SportsNet.Web.ViewModels.Categories;
	using SportsNet.Web.ViewModels.Category;
	using SportsNet.Web.ViewModels.Post;
	using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

	[Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

		public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            IEnumerable<CategoryAllViewModel> categories = await this.categoryService.GetCategories();

            return View(categories);
        }

        [HttpGet]
        public IActionResult Add()
        {
            try
            {
                CategoryFormModel model =  new CategoryFormModel();

                return View(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryFormModel model) 
        {
            bool categoryExists = categoryService.ExistsByNameAsync(model.Name.ToLower());
            if (categoryExists)
            {
                ModelState.AddModelError(nameof(model.Name), "Selected category already exists!");
            }

            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await this.categoryService.CreateAsync(model);

                return RedirectToAction("All", "Category");
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new category! Please try again later or contact administrator!");
                return View(model);
            }
        }

		[HttpGet()]
		[AllowAnonymous]
		public ActionResult Details([FromQuery] AllCategoriesQueryModel queryModel, int id)
		{
            bool categoryExists = this.categoryService.ExistsByIdAsync(id);
            if (!categoryExists) 
            {
				return RedirectToAction("All", "Category");
			}

			try
            {
                var queryResult = this.categoryService.GetDetailsByIdAsync(id, queryModel.CurrentPage, queryModel.PostsPerPage);

				queryModel.Category = queryResult.Category;
				queryModel.TotalPosts = queryResult.TotalPosts;
				queryModel.Posts = queryResult.Posts;

				return View(queryModel);
			}
            catch (Exception)
            {

                return BadRequest();
            }
		}
	}
}
