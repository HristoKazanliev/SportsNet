namespace SportsNet.Services.Data
{
	using System.Threading.Tasks;

	using Interfaces;
	using Web.ViewModels.Image;
	using SportsNet.Data.Models;
	using SportsNet.Data.Repositories.Interfaces;
    using System.Collections.Generic;
    using SportsNet.Services.Mapping;

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

        public async Task ApproveImageAsync(int imageId)
        {
            Image? image = this.GetImage(imageId);
            image.IsApproved = true;
            image.ModifiedOn = DateTime.Now.AddHours(3);

            await this.imageRepository.SaveChangesAsync();
        }

        public async Task RejectImageAsync(int imageId)
        {
            Image image = this.GetImage(imageId)!;
            this.imageRepository.Delete(image);

            await this.imageRepository.SaveChangesAsync();
        }

        public Image? GetImage(int imageId)
            => this.imageRepository.All()
            .Where(i => i.Id == imageId)
            .FirstOrDefault();

        public TModel GetImage<TModel>(int imageId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TModel> GetAllApprovedImages<TModel>()
            => this.imageRepository.All()
            .Where(i => i.IsApproved)
            .To<TModel>()
            .ToList();

        public IEnumerable<TModel> GetAllUnapprovedImages<TModel>()
            => this.imageRepository.All()
            .Where(i => !i.IsApproved)
            .To<TModel>()
            .ToList();
    }
}
