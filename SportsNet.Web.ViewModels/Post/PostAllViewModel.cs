namespace SportsNet.Web.ViewModels.Post
{
    using SportsNet.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Linq;

    public class PostAllViewModel
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
