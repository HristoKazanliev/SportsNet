namespace SportsNet.Services.Data.Models.Category
{
	using SportsNet.Web.ViewModels.Category;
	using SportsNet.Web.ViewModels.Post;

	public class AllCategoriesQueryServiceModel
	{
        public AllCategoriesQueryServiceModel()
        {
			this.Category = new CategoryAllViewModel();
			this.Posts = new HashSet<PostAllViewModel>();
		}

        public int TotalPosts { get; set; }

		public int CurrentPage { get; set; }

		public int PostsPerPage { get; set; }

		public CategoryAllViewModel Category { get; set; }

		public IEnumerable<PostAllViewModel> Posts { get; set; }
	}
}
