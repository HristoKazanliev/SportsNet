namespace SportsNet.Services.Data.Interfaces
{
    using SportsNet.Data.Models;
	using SportsNet.Web.ViewModels.Category;

	public interface ICategoryService
    {
        Task<IEnumerable<PostSelectCategoryFormModel>> AllCategoriesAsync();

        Task<IEnumerable<CategoryAllViewModel>> GetCategories();

		Task<int> CreateAsync(string name, string description, string imageUrl);

        Task EditCategoryAsync(int categoryId, string name, string description, string imageUrl);

        Task DeleteCategoryAsync(int categoryId);

        Task<IEnumerable<string>> AllCategoryNamesAsync();

		AllCategoriesQueryModel GetDetailsByIdAsync(int categoryId, int currentPage = 1, int postsPerPage = int.MaxValue);

		Task<CategoryDetailsViewModel> GetDetailsForName(int id);

        T GetCategoryById<T>(int categoryId);

        Category GetCategoryById(int categoryId);

        bool ExistsByNameAsync(string name);

		bool ExistsByIdAsync(int id);

	}
}
