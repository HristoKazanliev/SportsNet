namespace SportsNet.Services.Data.Interfaces
{
	using SportsNet.Web.ViewModels.Image;

	public interface IImageService
	{
		Task CreateImageAsync(ImageFormViewModel viewModel);
	}
}
