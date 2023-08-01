namespace SportsNet.Web.Infrastructure.Extensions
{
	using SportsNet.Web.ViewModels.Category;

	public static class ViewModelsExtensions
	{
		public static string GetUrlInformation(this ICategoryDetailsModel model)
		{
			return model.Name.Replace(" ", "-");
		}
	}
}
