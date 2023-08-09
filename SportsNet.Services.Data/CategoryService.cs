namespace SportsNet.Services.Data
{
    using Microsoft.EntityFrameworkCore;

    using SportsNet.Data.Models;
    using SportsNet.Data.Repositories.Interfaces;
    using SportsNet.Services.Data.Interfaces;
	using SportsNet.Services.Mapping;
	using SportsNet.Web.ViewModels.Category;
	using SportsNet.Web.ViewModels.Post;

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

        public async Task<int> CreateAsync(string name, string description, string imageUrl)
        {
            Category newCategory = new Category()
            {
                Name = name,
                Description = description,
                ImageUrl = imageUrl,
            };

            await this.categoriesRepository.AddAsync(newCategory);
            await this.categoriesRepository.SaveChangesAsync();

            return newCategory.Id;
        }

        public async Task EditCategoryAsync(int categoryId, string name, string description, string imageUrl)
        {
			Category category = this.GetCategoryById(categoryId);

			category.Name = name;
			category.Description = description;
			category.ImageUrl = imageUrl;

			await this.categoriesRepository.SaveChangesAsync();
		}

        public async Task DeleteCategoryAsync(int categoryId)
        {
            Category category = this.GetCategoryById(categoryId);

            category.IsDeleted = true;
            category.DeletedOn = DateTime.UtcNow.AddHours(3);

            await this.categoriesRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryAllViewModel>> GetCategories()
		{
			IEnumerable<CategoryAllViewModel> categories = await this.categoriesRepository
                .All()
                .Where(c => c.IsDeleted == false)
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
		
		public AllCategoriesQueryModel GetDetailsByIdAsync(int categoryId, int currentPage = 1, int postsPerPage = int.MaxValue)
		{
			IQueryable<Post> postQuery = this.postsRepository.All().Where(p => p.CategoryId == categoryId);

            int totalPosts = postQuery.ToList().Count;

			CategoryAllViewModel category = this.GetCategory(categoryId);

			var posts = this.GetPosts(postQuery);
			
			return new AllCategoriesQueryModel
			{
				TotalPosts = totalPosts,
				CurrentPage = currentPage,
				PostsPerPage = postsPerPage,
				Category = category,
				Posts = posts
			};
		}

        public T GetCategoryById<T>(int categoryId)
            => this.categoriesRepository.All()
            .Where(c => c.Id == categoryId)
            .To<T>()
            .FirstOrDefault()!;

        public Category GetCategoryById(int categoryId)
            => this.categoriesRepository.All()
            .Where(c => c.Id == categoryId)
            .FirstOrDefault()!;

        public bool ExistsByNameAsync(string name)
			=> this.categoriesRepository.All().Any(c => c.Name.ToLower() == name);

		public bool ExistsByIdAsync(int id)
		    => this.categoriesRepository.All().Any(c => c.Id == id);

        public async Task<CategoryDetailsViewModel> GetDetailsForName(int id)
        {
            Category category = await this.categoriesRepository.All().FirstAsync(c => c.Id == id);

            CategoryDetailsViewModel model = new CategoryDetailsViewModel
            {
                Id = id,
                Name = category.Name
            };

            return model;
        }

		private CategoryAllViewModel GetCategory(int categoryId)
            => this.categoriesRepository
            .All()
            .Where(c => c.Id == categoryId)
            .To<CategoryAllViewModel>()
            .FirstOrDefault()!;

		private IEnumerable<PostAllViewModel> GetPosts(IQueryable<Post> postQuery)
		  => postQuery
			  .To<PostAllViewModel>()
			  .ToList();       
    }
}
