namespace SportsNet.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;

    using SportsNet.Services.Data.Interfaces;
    using SportsNet.Web.ViewModels.Comment;
    using SportsNet.Data.Models;

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
    }
}
