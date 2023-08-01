namespace SportsNet.Web.ViewModels.Category
{
	public class CategoryDetailsViewModel : ICategoryDetailsModel
	{
		public int Id { get; set; }

		public string Name { get; set; } = null!;
	}
}
