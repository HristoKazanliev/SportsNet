namespace SportsNet.Services.Data
{
	using System.Threading.Tasks;

	using Interfaces;
	using Web.ViewModels.Image;
	using SportsNet.Data.Models;
	using SportsNet.Data.Repositories.Interfaces;
    using System.Collections.Generic;
    using SportsNet.Services.Mapping;
    using SportsNet.Data;

    public class ImageService : IImageService
	{
        private readonly SportsNetDbContext dbContext;
		//private readonly IRepository<Image> imageRepository;

        public ImageService(SportsNetDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateImageAsync(ImageFormViewModel viewModel)
		{
			Image image = new Image
			{
				Description = viewModel.Description,
				ImageUrl = viewModel.ImageUrl,
				AuthorId = Guid.Parse(viewModel.AuthorId!),
				CreatedOn = DateTime.UtcNow
			};

			await this.dbContext.Images.AddAsync(image);
			await this.dbContext.SaveChangesAsync();
		}

        public async Task ApproveImageAsync(int imageId)
        {
            Image image = this.GetImage(imageId);
            image.IsApproved = true;
            image.ModifiedOn = DateTime.UtcNow;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task RejectImageAsync(int imageId)
        {
            Image image = this.GetImage(imageId)!;
            this.dbContext.Images.Remove(image);

            await this.dbContext.SaveChangesAsync();
        }

        public Image GetImage(int imageId)
            => this.dbContext.Images
            .Where(i => i.Id == imageId)
            .FirstOrDefault()!;

        public TModel GetImage<TModel>(int imageId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TModel> GetAllApprovedImages<TModel>()
            => this.dbContext.Images
            .Where(i => i.IsApproved)
            .To<TModel>()
            .ToList();

        public IEnumerable<TModel> GetAllUnapprovedImages<TModel>()
            => this.dbContext.Images
            .Where(i => !i.IsApproved)
            .To<TModel>()
            .ToList();
    }
}
