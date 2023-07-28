namespace SportsNet.Web.ViewModels.Category
{
    using System.ComponentModel.DataAnnotations;

    using static Common.GeneralApplicationConstants;

    public class AllCategoriesQueryModel : CategoryAllViewModel
    {
        public AllCategoriesQueryModel()
        {
            this.CurrentPage = DefaultPage;
            this.PostsPerPage = EntitiesPerPage;

            this.Posts = new HashSet<string>();
		}

        public int CurrentPage { get; set; }

        [Display(Name = "Show Posts On Page")]
        public int PostsPerPage { get; set; }

        public int TotalPosts { get; set; }

        public IEnumerable<string> Posts { get; set; }
    }
}
