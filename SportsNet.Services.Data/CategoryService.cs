namespace SportsNet.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using SportsNet.Data.Models;
    using SportsNet.Data.Repositories.Interfaces;
    using SportsNet.Services.Data.Interfaces;
    using SportsNet.Web.ViewModels.Categories;
    using SportsNet.Web.ViewModels.Post;
    using System.Threading;

    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> categoriesRepository;

        public CategoryService(IRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public async Task<IEnumerable<PostSelectCategoryFormModel>> AllCategoriesAsync() 
            => await this.categoriesRepository
            .AllAsNoTracking()
            .Select(c => new PostSelectCategoryFormModel
            {
                Id = c.Id,
                Name = c.Name,
            })
            .ToArrayAsync();

        public async Task CreateAsync(CategoryFormModel formModel)
        {
            Category newCategory = new Category()
            {
                Name = formModel.Name,
                Description = formModel.Description,
                ImageUrl = formModel.ImageUrl,
            };

            await this.categoriesRepository.AddAsync(newCategory);
            await this.categoriesRepository.SaveChangesAsync();
        }

        public bool ExistsByNameAsync(string name) 
            => this.categoriesRepository.All().Any(c => c.Name.ToLower() == name);
        
    }
}
