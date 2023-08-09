namespace SportsNet.Web.ViewModels.Image
{
    using System.ComponentModel.DataAnnotations;

    using SportsNet.Data.Models;
    using SportsNet.Services.Mapping;

	using static Common.EntityValidationConstants.Image;

	public class ImageFormViewModel : IMapFrom<Image>
	{
        public int Id { get; set; }

		[Required]
		[StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
		public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

		public string? AuthorId { get; set; } 
	}
}
