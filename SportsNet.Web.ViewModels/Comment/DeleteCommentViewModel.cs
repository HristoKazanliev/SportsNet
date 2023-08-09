namespace SportsNet.Web.ViewModels.Comment
{
	using SportsNet.Data.Models;

	public class DeleteCommentViewModel 
	{
        public int Id { get; set; }

		public string Content { get; set; } = null!;

		public string PostId { get; set; } = null!;

		public ApplicationUser Author { get; set; } = null!;

		public DateTime CreatedOn { get; set; }
	}
}
