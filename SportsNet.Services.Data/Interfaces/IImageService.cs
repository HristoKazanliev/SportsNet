namespace SportsNet.Services.Data.Interfaces
{
    using SportsNet.Data.Models;
    using SportsNet.Web.ViewModels.Image;

	public interface IImageService
	{
		Task CreateImageAsync(ImageFormViewModel viewModel);

		Task ApproveImageAsync(int imageId);

        Task RejectImageAsync(int imageId);

		TModel GetImage<TModel>(int imageId);

		Image? GetImage(int imageId);

        IEnumerable<TModel> GetAllApprovedImages<TModel>();

        IEnumerable<TModel> GetAllUnapprovedImages<TModel>();
    }
}
