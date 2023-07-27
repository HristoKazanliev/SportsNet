namespace SportsNet.Web.ViewModels.Post
{
    using System.ComponentModel.DataAnnotations;

    using Enums;

    using static Common.GeneralApplicationConstants;

    public class AllPostsQueryModel
    {
        public AllPostsQueryModel()
        {
            this.CurrentPage = DefaultPage;
            this.PostsPerPage = EntitiesPerPage;

            this.Categories = new HashSet<string>();
            this.Posts = new HashSet<PostAllViewModel>();
        }

        public string? Category { get; set; }

        [Display(Name = "Search By Text")]
        public string? SearchTerm { get; set; }

        [Display(Name = "Sort Posts By")]
        public PostSorting PostSorting { get; set; }

        public int CurrentPage { get; set; }

        [Display(Name = "Show Posts On Page")]
        public int PostsPerPage { get; set; }

        public int TotalPosts { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<PostAllViewModel> Posts { get; set; }
    }
}
