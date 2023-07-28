namespace SportsNet.Services.Data.Interfaces
{
    using SportsNet.Web.ViewModels.Categories;
	using SportsNet.Web.ViewModels.Category;

	public interface ICategoryService
    {
        Task<IEnumerable<PostSelectCategoryFormModel>> AllCategoriesAsync();

        Task<IEnumerable<CategoryAllViewModel>> GetCategories();

		Task CreateAsync(CategoryFormModel formModel);

        Task<IEnumerable<string>> AllCategoryNamesAsync();

        bool ExistsByNameAsync(string name);
    }
}
