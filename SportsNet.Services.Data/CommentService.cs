namespace SportsNet.Services.Data
{
    using SportsNet.Data.Models;
    using SportsNet.Data.Repositories.Interfaces;
    using SportsNet.Services.Data.Interfaces;

    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> commentRepository;

        public CommentService(IRepository<Comment> commentRepository)
        {
            this.commentRepository = commentRepository;
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
    }
}
