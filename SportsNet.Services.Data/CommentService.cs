namespace SportsNet.Services.Data
{
	using Interfaces;
    using SportsNet.Data;
    using SportsNet.Data.Models;
    using SportsNet.Data.Repositories.Interfaces;
	using SportsNet.Web.ViewModels.Comment;

	public class CommentService : ICommentService
    {
        //private readonly IRepository<Comment> commentRepository;
        //private readonly IRepository<ApplicationUser> userRepository;
        private readonly SportsNetDbContext dbContext;

        public CommentService(SportsNetDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateCommentAsync(string postId, string userId, string content)
        {
            var comment = new Comment()
            {
                PostId = Guid.Parse(postId),
                AuthorId = Guid.Parse(userId),
                Content = content,
                CreatedOn = DateTime.UtcNow,
            };

            await this.dbContext.Comments.AddAsync(comment);
            await this.dbContext.SaveChangesAsync();
        }

		public async Task DeleteCommentAsync(string postId, int commentId)
		{
            Comment comment = this.GetById(postId, commentId);

            this.dbContext.Comments.Remove(comment);
            await this.dbContext.SaveChangesAsync();
		}

        public Comment GetById(string postId, int commentId)
            => this.dbContext.Comments
                .FirstOrDefault(c => c.PostId == Guid.Parse(postId) &&
                                    c.Id == commentId)!;

		public async Task<DeleteCommentViewModel> GetCommentForDeleteByIdAsync(string postId, int commentId)
		{
			Comment comment = this.dbContext.Comments
                .First(c => c.PostId == Guid.Parse(postId) && c.Id == commentId);

            ApplicationUser user = this.dbContext.Users
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
