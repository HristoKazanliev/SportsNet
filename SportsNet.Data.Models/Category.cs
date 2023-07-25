namespace SportsNet.Data.Models
{
    using Data.Common;
    using System.ComponentModel.DataAnnotations;

    using static SportsNet.Common.EntityValidationConstants.Category;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Posts = new HashSet<Post>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        public ICollection<Post> Posts { get; set; }
    }
}