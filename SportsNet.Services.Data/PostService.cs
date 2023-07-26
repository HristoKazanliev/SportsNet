namespace SportsNet.Services.Data
{
    using Interfaces;
    using SportsNet.Data;
    using SportsNet.Data.Models;
    using SportsNet.Data.Models.Enums;
    using SportsNet.Data.Repositories.Interfaces;
    using SportsNet.Web.ViewModels.Post;
    

    public class PostService : IPostService
    {
        private readonly IRepository<Post> postRepository;

        public PostService(IRepository<Post> postRepository)
        {
            this.postRepository = postRepository;
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
