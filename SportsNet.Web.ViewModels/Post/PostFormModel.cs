namespace SportsNet.Web.ViewModels.Post
{
    using System.ComponentModel.DataAnnotations;
    using SportsNet.Data.Models;
    using SportsNet.Data.Models.Enums;
    using SportsNet.Services.Mapping;
    using SportsNet.Web.ViewModels.Category;

    using static Common.EntityValidationConstants.Post;

    public class PostFormModel : IMapFrom<Post>, IMapFrom<Category>
    {
        public PostFormModel()
        {
            this.Categories = new HashSet<PostSelectCategoryFormModel>();
            this.Types = new HashSet<PostType>();
        }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
        public string Content { get; set; } = null!;

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<PostSelectCategoryFormModel> Categories { get; set; }

        [Required]
        [Display(Name = "Post Type")]
        public PostType Type { get; set; }

        public IEnumerable<PostType> Types { get; set; }
    }
}
