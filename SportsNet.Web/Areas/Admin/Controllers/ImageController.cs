namespace SportsNet.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using SportsNet.Services.Data.Interfaces;
    using SportsNet.Web.ViewModels.Image;

    public class ImageController : BaseAdminController
    {
        private readonly IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

        public IActionResult All()
        {
            IEnumerable<ImageAllViewModel> allImages = this.imageService.GetAllUnapprovedImages<ImageAllViewModel>();

            return View(allImages);
        }

        public async Task<IActionResult> Approve(int imageId)
        {
            await this.imageService.ApproveImageAsync(imageId);

            return Redirect("All");
        }

        public async Task<IActionResult> Reject(int imageId)
        {
            await this.imageService.RejectImageAsync(imageId);

            return Redirect("All");
        }
    }
}
