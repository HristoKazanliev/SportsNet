namespace SportsNet.Web.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Identity;
	using SportsNet.Services.Data.Interfaces;
	using SportsNet.Data.Models;

	using static Common.NotificationMessagesConstants;
	using static SportsNet.Common.EntityValidationConstants;

	[Authorize]
	//[Route("api/[controller]")]
	public class VoteController : Controller
	{
		private readonly IVoteService voteService;
		private readonly UserManager<ApplicationUser> userManager;

        public VoteController(IVoteService voteService, UserManager<ApplicationUser> userManager)
        {
            this.voteService = voteService;
			this.userManager = userManager;
        }

		[HttpPost]
        public async Task<IActionResult> VoteConfirmation(string id)
		{
			var user = this.userManager.GetUserId(this.User);

			try
			{
				await this.voteService.VoteAsync(id, user);

				//var updatedVoteCount = this.voteService.GetVotes(id);

				return RedirectToAction("Details", "Post", new { id });
			}
			catch (Exception)
			{
				TempData[ErrorMessage] =
								"Unexpected error occurred! Please try again later or contact administrator";

				return RedirectToAction("All", "Post");

			}
			
		}
	}
}
