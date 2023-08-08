namespace SportsNet.Web.ViewModels.Category
{
    using SportsNet.Data.Models;
    using SportsNet.Services.Mapping;
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.Category;

    public class CategoryFormModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(ImageUrlMaxLength)]
        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; } = null!;
    }
}
