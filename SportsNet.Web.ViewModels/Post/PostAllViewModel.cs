namespace SportsNet.Web.ViewModels.Post
{
    using SportsNet.Data.Models;
	using SportsNet.Services.Mapping;
	using System.ComponentModel.DataAnnotations;

    public class PostAllViewModel : IMapFrom<Post>
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        [Display(Name = "Comments")]
        public int CommentsCount { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        public ApplicationUser Author { get; set; } = null!;

        public string Type { get; set; } = null!;
    }
}
