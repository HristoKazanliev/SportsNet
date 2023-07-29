namespace SportsNet.Web.ViewModels.Comment
{
	using SportsNet.Data.Models;
	using SportsNet.Services.Mapping;

	public class CommentsInfoOnPostViewModel : IMapFrom<Comment>
	{
        public int Id { get; set; }

		public string Author { get; set; } = null!;

		public string Content { get; set; } = null!;

		public DateTime CreatedOn { get; set; }
    }
}
