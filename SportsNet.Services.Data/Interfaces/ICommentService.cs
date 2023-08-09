namespace SportsNet.Services.Data.Interfaces
{
	using SportsNet.Data.Models;
	using SportsNet.Web.ViewModels.Comment;

	public interface ICommentService
    {
        Task CreateCommentAsync(string postId, string userId, string content);

        Task DeleteCommentAsync(string postId, int commentId);

		Comment GetById(string postId, int commentId);

		Task<DeleteCommentViewModel> GetCommentForDeleteByIdAsync(string postId, int commentId);
	}
}
