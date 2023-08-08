namespace SportsNet.Web.Areas.Admin.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using SportsNet.Data.Models;
    using SportsNet.Services.Data.Interfaces;
    using SportsNet.Web.ViewModels.Category;

    public class CategoryController : BaseAdminController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

		[HttpGet]
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
                CategoryFormModel model = new CategoryFormModel();

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
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
                int categoryId = await this.categoryService.CreateAsync(model.Name, model.Description, model.ImageUrl);

                return RedirectToAction("Details", "Category", new { area = "", id = categoryId, information = model.Name });
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new category! Please try again later!");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            CategoryFormModel category = this.categoryService.GetCategoryById<CategoryFormModel>(id);
            if (category == null)
            {
                //this.ModelState.AddModelError(string.Empty, "Category with the provided id does not exist!");
                //return RedirectToAction("All", "Category");
				return this.NotFound();
			}

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryFormModel model)
        {
			if (!this.ModelState.IsValid)
			{
				return this.View(model);
			}

            await this.categoryService.EditCategoryAsync(model.Id, model.Name, model.Description, model.ImageUrl);

			return this.RedirectToAction("All", "Category");
		}
	}
}
