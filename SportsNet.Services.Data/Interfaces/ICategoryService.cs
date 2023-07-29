namespace SportsNet.Services.Data.Interfaces
{
	using SportsNet.Services.Data.Models.Post;
	using SportsNet.Web.ViewModels.Categories;
	using SportsNet.Web.ViewModels.Category;

	public interface ICategoryService
    {
        Task<IEnumerable<PostSelectCategoryFormModel>> AllCategoriesAsync();

        Task<IEnumerable<CategoryAllViewModel>> GetCategories();

		Task CreateAsync(CategoryFormModel formModel);

        Task<IEnumerable<string>> AllCategoryNamesAsync();

		AllCategoriesQueryModel GetDetailsByIdAsync(int categoryId, int currentPage = 1, int postsPerPage = int.MaxValue);

		bool ExistsByNameAsync(string name);

		bool ExistsByIdAsync(int id);

	}
}
