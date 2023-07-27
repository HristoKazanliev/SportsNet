namespace SportsNet.Services.Data
{
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using SportsNet.Data;
    using SportsNet.Data.Models;
    using SportsNet.Data.Models.Enums;
    using SportsNet.Data.Repositories.Interfaces;
    using SportsNet.Services.Data.Models.Post;
    using SportsNet.Web.ViewModels.Post;
    using SportsNet.Web.ViewModels.Post.Enums;

    public class PostService : IPostService
    {
        private readonly IRepository<Post> postRepository;

        public PostService(IRepository<Post> postRepository)
        {
            this.postRepository = postRepository;
        }

        public async Task<AllPostsQueryServiceModel> AllAsync(AllPostsQueryModel queryModel)
        {
            IQueryable<Post> postsQuery = this.postRepository.All().AsQueryable();

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
                _ => postsQuery.OrderBy(p => p.Comments.Count)
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

        public async Task CreateAsync(PostFormModel formModel, string userId)
        {
            Enum.TryParse(typeof(PostType), formModel.Type.ToString(), out object typeResult);

            Post post = new Post()
            {
                Title = formModel.Title,
                Content = formModel.Content,
                CategoryId = formModel.CategoryId,
                AuthorId = Guid.Parse(userId),
                Type = (PostType)typeResult!
            };

            await this.postRepository.AddAsync(post);
            await this.postRepository.SaveChangesAsync();
        }

        public IEnumerable<PostType> GetPostTypes() 
            => Enum.GetValues(typeof(PostType))
            .Cast<PostType>()
            .ToList();

        
    }
}
