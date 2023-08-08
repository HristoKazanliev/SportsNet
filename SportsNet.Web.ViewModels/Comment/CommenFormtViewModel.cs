namespace SportsNet.Web.ViewModels.Comment
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.Comment;

    public class CommenFormtViewModel
    {
        public string PostId { get; set; } = null!;

        [Required]
        [StringLength(MaxLength, MinimumLength = MinLength)]
        public string Content { get; set; } = null!;
    }
}
