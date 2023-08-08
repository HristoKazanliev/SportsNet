namespace SportsNet.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
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
    }
}
