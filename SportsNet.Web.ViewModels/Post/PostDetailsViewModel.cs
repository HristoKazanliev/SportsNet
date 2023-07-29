namespace SportsNet.Web.ViewModels.Post
{
	using AutoMapper;
	using SportsNet.Data.Models;
    using SportsNet.Services.Mapping;
    using SportsNet.Web.ViewModels.Comment;
	using System.ComponentModel.DataAnnotations;
	using System.Xml.Linq;

	public class PostDetailsViewModel : IMapFrom<Post>, IMapFrom<Category>, IHaveCustomMappings
	{
        public PostDetailsViewModel()
        {
			this.Comments = new HashSet<CommentsInfoOnPostViewModel>();
        }

        public string Id { get; set; } = null!;

		public string Title { get; set; } = null!;

		public string Content { get; set; } = null!;

		public string Type { get; set; } = null!;

		public int VotesCount { get; set; }

		public int CategoryId { get; set;}

		public Category Category { get; set; } = null!;

		public ApplicationUser Author { get; set; } = null!;

		[Display(Name = "Created On")]
		public DateTime CreatedOn { get; set; }

		public IEnumerable<CommentsInfoOnPostViewModel> Comments { get; set; }

		public void CreateMappings(IProfileExpression config)
		{
			config.CreateMap<Post, PostDetailsViewModel>()
				.ForMember(p => p.VotesCount, options =>
				{
					options.MapFrom(p => p.Votes.Sum(v => (int)v.Type));
				});
		}
	}
}
