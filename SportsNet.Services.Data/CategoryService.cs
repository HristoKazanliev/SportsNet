namespace SportsNet.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SportsNet.Data;
    using SportsNet.Data.Models;
    using SportsNet.Data.Repositories.Interfaces;
    using SportsNet.Services.Data.Interfaces;
	using SportsNet.Services.Mapping;
	using SportsNet.Web.ViewModels.Category;
	using SportsNet.Web.ViewModels.Post;

    public class CategoryService : ICategoryService
    {
        //private readonly IRepository<Category> categoriesRepository;
        //private readonly IRepository<Post> postsRepository;
        private readonly SportsNetDbContext dbContext;

        public CategoryService(SportsNetDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<PostSelectCategoryFormModel>> AllCategoriesAsync() 
            => await this.dbContext.Categories
            .Select(c => new PostSelectCategoryFormModel
            {
                Id = c.Id,
                Name = c.Name,
            })
            .ToArrayAsync();

        public async Task<IEnumerable<string>> AllCategoryNamesAsync()
        {
            IEnumerable<string> allNames = await this.dbContext.Categories
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

            await this.dbContext.Categories.AddAsync(newCategory);
            await this.dbContext.SaveChangesAsync();

            return newCategory.Id;
        }

        public async Task EditCategoryAsync(int categoryId, string name, string description, string imageUrl)
        {
			Category category = this.GetCategoryById(categoryId);

			category.Name = name;
			category.Description = description;
			category.ImageUrl = imageUrl;
            category.ModifiedOn = DateTime.Now;

			await this.dbContext.SaveChangesAsync();
		}

        public async Task DeleteCategoryAsync(int categoryId)
        {
            Category category = this.GetCategoryById(categoryId);

            category.IsDeleted = true;
            category.DeletedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryAllViewModel>> GetCategories()
		{
			IEnumerable<CategoryAllViewModel> categories = await this.dbContext
                .Categories
                .Where(c => c.IsDeleted == false)
                .Select(c => new CategoryAllViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    ImageUrl = c.ImageUrl,
                    PostsCount = this.dbContext.Posts.Where(p => p.CategoryId == c.Id).Count()
                })
                .ToArrayAsync();

            return categories;
		}
		
		public AllCategoriesQueryModel GetDetailsByIdAsync(int categoryId, int currentPage = 1, int postsPerPage = int.MaxValue)
		{
			IQueryable<Post> postQuery = this.dbContext.Posts.Where(p => p.CategoryId == categoryId);

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
            => this.dbContext.Categories
            .Where(c => c.Id == categoryId)
            .To<T>()
            .FirstOrDefault()!;

        public Category GetCategoryById(int categoryId)
            => this.dbContext.Categories
            .Where(c => c.Id == categoryId)
            .FirstOrDefault()!;

        public bool ExistsByNameAsync(string name)
			=> this.dbContext.Categories.Any(c => c.Name.ToLower() == name);

		public bool ExistsByIdAsync(int id)
		    => this.dbContext.Categories.Any(c => c.Id == id);

        public async Task<CategoryDetailsViewModel> GetDetailsForName(int id)
        {
            Category category = await this.dbContext.Categories.FirstAsync(c => c.Id == id);

            CategoryDetailsViewModel model = new CategoryDetailsViewModel
            {
                Id = id,
                Name = category.Name
            };

            return model;
        }

		private CategoryAllViewModel GetCategory(int categoryId)
            => this.dbContext.Categories
            .Where(c => c.Id == categoryId)
            .To<CategoryAllViewModel>()
            .FirstOrDefault()!;

		private IEnumerable<PostAllViewModel> GetPosts(IQueryable<Post> postQuery)
		  => postQuery
			  .To<PostAllViewModel>()
			  .ToList();       
    }
}
