namespace SportsNet.Services.Data.Interfaces
{
    using SportsNet.Web.ViewModels.Categories;

    public interface ICategoryService
    {
        Task<IEnumerable<PostSelectCategoryFormModel>> AllCategoriesAsync();

        Task CreateAsync(CategoryFormModel formModel);

        bool ExistsByNameAsync(string name);
    }
}
