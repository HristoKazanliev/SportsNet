namespace SportsNet.Web.ViewModels.Image
{
	using System.ComponentModel.DataAnnotations;

	using static Common.EntityValidationConstants.Image;

	public class ImageFormViewModel
	{
        public int Id { get; set; }

		[Required]
		[StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
		public string Description { get; set; } = null!;

		[MaxLength(UrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

		public string AuthorId { get; set; } = null!;
	}
}
