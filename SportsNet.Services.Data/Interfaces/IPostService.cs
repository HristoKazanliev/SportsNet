namespace SportsNet.Services.Data.Interfaces
{
	using SportsNet.Data.Models;
	using SportsNet.Data.Models.Enums;
    using SportsNet.Services.Data.Models.Post;
    using SportsNet.Web.ViewModels.Post;

    public interface IPostService
    {
        IEnumerable<PostType> GetPostTypes();

        Task<string> CreateAsync(PostFormModel formModel, string userId);

        Task<AllPostsQueryServiceModel> AllAsync(AllPostsQueryModel queryModel);

		Task<bool> ExistsByIdAsync(string postId);

        Task<bool> IsUserOwner(string postId, string userId);

        Task EditPostAsync(PostFormModel model, string postId);

        Task<Post> GetById(string postId);

        TModel GetPost<TModel>(string postId);
	}
}
