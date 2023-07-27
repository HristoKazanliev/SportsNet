namespace SportsNet.Services.Data.Interfaces
{
    using SportsNet.Data.Models.Enums;
    using SportsNet.Services.Data.Models.Post;
    using SportsNet.Web.ViewModels.Post;

    public interface IPostService
    {
        IEnumerable<PostType> GetPostTypes();

        Task CreateAsync(PostFormModel formModel, string userId);

        Task<AllPostsQueryServiceModel> AllAsync(AllPostsQueryModel queryModel);
    }
}
