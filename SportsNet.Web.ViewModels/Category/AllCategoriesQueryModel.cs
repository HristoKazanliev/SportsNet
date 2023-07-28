namespace SportsNet.Web.ViewModels.Category
{
	using SportsNet.Web.ViewModels.Post;
	using System.ComponentModel.DataAnnotations;

    using static Common.GeneralApplicationConstants;

    public class AllCategoriesQueryModel
    {
        public AllCategoriesQueryModel()
        {
            this.CurrentPage = DefaultPage;
            this.PostsPerPage = EntitiesPerPage;

            this.Category = new CategoryAllViewModel();
			this.Posts = new HashSet<PostAllViewModel>();
		}

        public int CurrentPage { get; set; }

        [Display(Name = "Show Posts On Page")]
        public int PostsPerPage { get; set; }

        public int TotalPosts { get; set; }

        public CategoryAllViewModel Category { get; set; }

        public IEnumerable<PostAllViewModel> Posts { get; set; }
    }
}
