namespace SportsNet.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
	using SportsNet.Services.Data.Interfaces;
	using SportsNet.Web.Infrastructure.Extensions;
	using SportsNet.Web.ViewModels.Category;

    using static SportsNet.Common.NotificationMessagesConstants;

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

		[HttpGet()]
		[AllowAnonymous]
		public async Task<ActionResult> Details([FromQuery] AllCategoriesQueryModel queryModel, int id, string information)
		{
            bool categoryExists = this.categoryService.ExistsByIdAsync(id);
            if (!categoryExists) 
            {
                TempData[ErrorMessage] = "Category with the provided id does not exist!";
                return RedirectToAction("All", "Category");
			}

            CategoryDetailsViewModel model = await this.categoryService.GetDetailsForName(id);
            if (model.GetUrlInformation() != information)
            {
				return RedirectToAction("Error", "Home");
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
				return RedirectToAction("Error", "Home");
			}
		}
	}
}
