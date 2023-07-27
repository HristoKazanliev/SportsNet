namespace SportsNet.Services.Data.Models.Post
{
    using SportsNet.Web.ViewModels.Post;

    public class AllPostsQueryServiceModel
    {
        public AllPostsQueryServiceModel()
        {
            this.Posts = new HashSet<PostAllViewModel>();
        }

        public int TotalPostsCount { get; set; }

        public IEnumerable<PostAllViewModel> Posts { get; set; }
    }
}
