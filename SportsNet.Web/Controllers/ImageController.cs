namespace SportsNet.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Data.Models;
    using Services.Data.Interfaces;
    using ViewModels.Image;

    using static Common.NotificationMessagesConstants;

    [Authorize]
	public class ImageController : Controller
	{
		private readonly IImageService imageService;
        private readonly UserManager<ApplicationUser> userManager;

        public ImageController(IImageService imageService, UserManager<ApplicationUser> userManager)
        {
            this.imageService = imageService;
            this.userManager = userManager;
        }

		[HttpGet]
		public IActionResult Add() 
		{ 
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(ImageFormViewModel model) 
		{ 
			ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            model.AuthorId = user.Id.ToString();

            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await this.imageService.CreateImageAsync(model);
                TempData[SuccessMessage] = "Image was added successfully!";

                return RedirectToAction("All", "Category");
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new image! Please try again later!");
                return View(model);
            }
		}
    }
}
