namespace SportsNet.Services.Data.Interfaces
{
    public interface ICommentService
    {
        Task CreateCommentAsync(string postId, string userId, string content);
    }
}
