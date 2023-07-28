namespace SportsNet.Web.ViewModels.Category
{
	using SportsNet.Data.Models;
	using SportsNet.Services.Mapping;

	public class CategoryAllViewModel : IMapFrom<Category>
	{
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

		public string ImageUrl { get; set; } = null!;

		public int PostsCount { get; set; }
	}
}
