namespace SportsNet.Services.Data
{
	using Microsoft.EntityFrameworkCore;

	using Interfaces;
	using SportsNet.Data.Models;
	using SportsNet.Data.Models.Enums;
	using SportsNet.Data.Repositories.Interfaces;
	using SportsNet.Services.Data.Models.Post;
	using SportsNet.Services.Mapping;
	using SportsNet.Web.ViewModels.Post;
	using SportsNet.Web.ViewModels.Post.Enums;
    using SportsNet.Data;

    public class PostService : IPostService
	{
		//private readonly IRepository<Post> postRepository;
		private readonly SportsNetDbContext dbContext;

		public PostService(SportsNetDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<AllPostsQueryServiceModel> AllAsync(AllPostsQueryModel queryModel)
		{
			IQueryable<Post> postsQuery = this.dbContext.Posts.AsQueryable();

			if (!string.IsNullOrWhiteSpace(queryModel.Category))
			{
				postsQuery = postsQuery
					.Where(p => p.Category.Name == queryModel.Category);
			}

			if (!string.IsNullOrWhiteSpace(queryModel.SearchTerm))
			{
				string wildCard = $"%{queryModel.SearchTerm.ToLower()}%";

				postsQuery = postsQuery
					.Where(p => EF.Functions.Like(p.Title, wildCard) ||
								EF.Functions.Like(p.Author.UserName, wildCard) ||
								EF.Functions.Like(p.Category.Name, wildCard));
			}

			postsQuery = queryModel.PostSorting switch
			{
				PostSorting.Newest => postsQuery
					.OrderByDescending(p => p.CreatedOn),
				PostSorting.Oldest => postsQuery
					.OrderBy(p => p.CreatedOn),
				_ => postsQuery.OrderBy(p => p.CreatedOn)
			};

			IEnumerable<PostAllViewModel> allPosts = await postsQuery
				.Where(p => !p.IsDeleted)
				.Skip((queryModel.CurrentPage - 1) * queryModel.PostsPerPage)
				.Take(queryModel.PostsPerPage)
				.Select(p => new PostAllViewModel()
				{
					Id = p.Id.ToString(),
					Title = p.Title,
					CommentsCount = p.Comments.Count,
					CreatedOn = p.CreatedOn,
					Author = p.Author,
					Type = p.Type.ToString()
				})
				.ToArrayAsync();

			int totalPosts = postsQuery.Count();

			return new AllPostsQueryServiceModel()
			{
				TotalPostsCount = totalPosts,
				Posts = allPosts
			};
		}

		public async Task<string> CreateAsync(PostFormModel formModel, string userId)
		{
			Enum.TryParse(typeof(PostType), formModel.Type.ToString(), out object? typeResult);

			Post post = new Post()
			{
				Id = Guid.NewGuid(),
				Title = formModel.Title,
				Content = formModel.Content,
				CategoryId = formModel.CategoryId,
				AuthorId = Guid.Parse(userId),
				Type = (PostType)typeResult!
			};

			await this.dbContext.Posts.AddAsync(post);
			await this.dbContext.SaveChangesAsync();

			return post.Id.ToString();
		}

		public async Task<bool> ExistsByIdAsync(string postId)
		{
			bool result = await this.dbContext.Posts
				.Where(p => p.IsDeleted == false)
				.AnyAsync(p => p.Id == Guid.Parse(postId));

			return result;
		}

        public async Task<bool> IsUserOwner(string postId, string userId)
        {
			Post post = await this.dbContext.Posts
				.Where(p => p.IsDeleted == false)
				.FirstAsync(p => p.Id.ToString() == postId);

			return post.AuthorId.ToString() == userId;
        }

        public async Task EditPostAsync(PostFormModel model, string postId)
        {
            Post post = await this.GetById(postId);

			post.Title = model.Title; 
			post.Content = model.Content;
			post.CategoryId = model.CategoryId;
			post.Type = model.Type;
			post.ModifiedOn = DateTime.UtcNow;

			await this.dbContext.SaveChangesAsync();
        }

        public async Task DeletePostAsync(string postId)
        {
            Post post =  await this.GetById(postId);

			post.IsDeleted = true;
			post.DeletedOn = DateTime.UtcNow;

			await this.dbContext.SaveChangesAsync();
        }

        public async Task<Post> GetById(string postId)
			=> await this.dbContext.Posts
			.Where(p => !p.IsDeleted)
			.FirstAsync(p => p.Id == Guid.Parse(postId));

        public TModel GetPost<TModel>(string postId)
			=> this.dbContext.Posts
			.Where(p => p.Id == Guid.Parse(postId) && !p.IsDeleted)
			.To<TModel>()
			.FirstOrDefault()!;

        public IEnumerable<PostType> GetPostTypes()
			=>  Enum.GetValues(typeof(PostType))
			.Cast<PostType>()
			.ToList();

      
    }
}
