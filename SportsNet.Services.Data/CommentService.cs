namespace SportsNet.Services.Data
{
	using Interfaces;
    using SportsNet.Data.Models;
    using SportsNet.Data.Repositories.Interfaces;
	using SportsNet.Web.ViewModels.Comment;

	public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> commentRepository;
        private readonly IRepository<ApplicationUser> userRepository;

        public CommentService(IRepository<Comment> commentRepository, IRepository<ApplicationUser> userRepository)
        {
            this.commentRepository = commentRepository;
            this.userRepository = userRepository;   
        }

        public async Task CreateCommentAsync(string postId, string userId, string content)
        {
            var comment = new Comment()
            {
                PostId = Guid.Parse(postId),
                AuthorId = Guid.Parse(userId),
                Content = content,
                CreatedOn = DateTime.UtcNow.AddHours(3),
            };

            await this.commentRepository.AddAsync(comment);
            await this.commentRepository.SaveChangesAsync();
        }

		public async Task DeleteCommentAsync(string postId, int commentId)
		{
            Comment comment = this.GetById(postId, commentId);

            this.commentRepository.Delete(comment);
            await this.commentRepository.SaveChangesAsync();
		}

        public Comment GetById(string postId, int commentId)
            => this.commentRepository.All()
                .FirstOrDefault(c => c.PostId == Guid.Parse(postId) &&
                                    c.Id == commentId)!;

		public async Task<DeleteCommentViewModel> GetCommentForDeleteByIdAsync(string postId, int commentId)
		{
			Comment comment = this.commentRepository.All()
                .First(c => c.PostId == Guid.Parse(postId) && c.Id == commentId);

            ApplicationUser user = userRepository.All()
                .Where(u => u.Id == comment.AuthorId).FirstOrDefault()!;

            return new DeleteCommentViewModel
            {
                Id = comment.Id,
                PostId = postId,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                Author = user
            };
		}
	}
}
