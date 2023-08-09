namespace SportsNet.Services.Data
{
	using System.Threading.Tasks;

	using Interfaces;
	using Web.ViewModels.Image;
	using SportsNet.Data.Models;
	using SportsNet.Data.Repositories.Interfaces;

	public class ImageService : IImageService
	{
		private readonly IRepository<Image> imageRepository;

        public ImageService(IRepository<Image> imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        public async Task CreateImageAsync(ImageFormViewModel viewModel)
		{
			Image image = new Image
			{
				Description = viewModel.Description,
				ImageUrl = viewModel.ImageUrl,
				AuthorId = Guid.Parse(viewModel.AuthorId!),
				CreatedOn = DateTime.Now.AddHours(3)
			};

			await this.imageRepository.AddAsync(image);
			await this.imageRepository.SaveChangesAsync();
		}
	}
}
