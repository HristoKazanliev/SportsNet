namespace SportsNet.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using SportsNet.Data.Models;
    using SportsNet.Data.Repositories.Interfaces;
    using SportsNet.Services.Data.Interfaces;
    using SportsNet.Web.ViewModels.Categories;

    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> categoriesRepository;
        

        public CategoryService(IRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public async Task<IEnumerable<PostSelectCategoryFormModel>> AllCategoriesAsync() 
            => await this.categoriesRepository.AllAsNoTracking().Select(c => new PostSelectCategoryFormModel
            {
                Id = c.Id,
                Name = c.Name,
            })
            .ToArrayAsync();
    }
}
