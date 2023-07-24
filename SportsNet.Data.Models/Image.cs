namespace SportsNet.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using SportsNet.Data.Common;

    using static SportsNet.Common.EntityValidationConstants.Image;

    public class Image : BaseModel<int>
    {
        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(UrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        public Guid AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; } = null!;

        public bool IsApproved { get; set; }
    }
}
