namespace SportsNet.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using SportsNet.Data.Models;
    using SportsNet.Data.Repositories.Interfaces;
    using SportsNet.Services.Data.Interfaces;
    using SportsNet.Web.ViewModels.Categories;
	using SportsNet.Web.ViewModels.Category;
	using SportsNet.Web.ViewModels.Post;
    using System.Threading;

    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> categoriesRepository;
        private readonly IRepository<Post> postsRepository;

        public CategoryService(IRepository<Category> categoriesRepository, IRepository<Post> postsRepository)
        {
            this.categoriesRepository = categoriesRepository;
            this.postsRepository = postsRepository;
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

        public async Task<IEnumerable<string>> AllCategoryNamesAsync()
        {
            IEnumerable<string> allNames = await this.categoriesRepository
                .All()
                .Select(c => c.Name)
                .ToArrayAsync();

            return allNames;
        }

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

		public async Task<IEnumerable<CategoryAllViewModel>> GetCategories()
		{
			IEnumerable<CategoryAllViewModel> categories = await this.categoriesRepository
                .All()
                .Select(c => new CategoryAllViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    ImageUrl = c.ImageUrl,
                    PostsCount = postsRepository.All().Where(p => p.CategoryId == c.Id).Count()
                })
                .ToArrayAsync();

            return categories;
		}

		public bool ExistsByNameAsync(string name)
			=> this.categoriesRepository.All().Any(c => c.Name.ToLower() == name);
	}
}
