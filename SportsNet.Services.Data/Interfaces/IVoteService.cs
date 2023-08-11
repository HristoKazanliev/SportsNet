namespace SportsNet.Services.Data.Interfaces
{
	public interface IVoteService
	{
		Task VoteAsync(string postId, string userId);

		int GetVotes(string postId);
	}
}
