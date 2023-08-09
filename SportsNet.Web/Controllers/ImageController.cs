namespace SportsNet.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using SportsNet.Services.Data.Interfaces;

	public class ImageController : Controller
	{
		private readonly IImageService imageService;

        public ImageController(IImageService imageService)
        {
            this.imageService = imageService;
        }

		[HttpGet]
		public IActionResult Add() 
		{ 
			return View();
		}


    }
}
