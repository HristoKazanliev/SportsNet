namespace SportsNet.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;

    using SportsNet.Services.Data.Interfaces;
    using SportsNet.Web.ViewModels.Comment;
    using SportsNet.Data.Models;
	using SportsNet.Web.Infrastructure.Extensions;

	using static Common.NotificationMessagesConstants;

	public class CommentController : Controller
    {
        private readonly ICommentService commentService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentController(ICommentService commentService, UserManager<ApplicationUser> userManager)
        {
            this.commentService = commentService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(CommenFormtViewModel comment)
        {
            ApplicationUser user = await this.userManager.GetUserAsync(this.User);

            if (string.IsNullOrWhiteSpace(comment.Content))
            {
                return this.RedirectToAction("Details", "Post", new { id = comment.PostId });
            }

            await this.commentService.CreateCommentAsync(comment.PostId, user.Id.ToString(), comment.Content);

            return this.RedirectToAction("Details", "Post", new { id = comment.PostId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string postId, int commentId)
        {
            Comment commentModel = this.commentService.GetById(postId, commentId);
            if (commentModel == null) 
            { 
                return this.NotFound();
            }

			ApplicationUser user = await this.userManager.GetUserAsync(this.User);
            if (user.Id != commentModel.AuthorId && !this.User.IsAdmin())
            {
				TempData[ErrorMessage] = "You must be admin or owner of the post you want to delete!";
				return RedirectToAction("Details", "Post", new { id = postId });
			}

			DeleteCommentViewModel model = await this.commentService.GetCommentForDeleteByIdAsync(postId, commentId);

			return this.View(model);
		}

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(string postId, int commentId)
        {
            await this.commentService.DeleteCommentAsync(postId, commentId);
            TempData[SuccessMessage] = "The comment was successfully deleted!";

            return RedirectToAction("Details", "Post", new { id = postId });
        }
    }
}
