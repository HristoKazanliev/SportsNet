namespace SportsNet.Services.Data
{
	using System.Threading.Tasks;
    using SportsNet.Data;
    using SportsNet.Data.Models;
	using SportsNet.Data.Models.Enums;
	using SportsNet.Data.Repositories.Interfaces;
	using SportsNet.Services.Data.Interfaces;

	public class VoteService : IVoteService
	{
		//private readonly IRepository<Vote> voteRepository;
		private readonly SportsNetDbContext dbContext;

        public VoteService(SportsNetDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

		public async Task VoteAsync(string postId, string userId)
		{
			Vote? vote = this.GetVote(postId, userId);
			if (vote != null) 
			{
				this.dbContext.Votes.Remove(vote);
			}
			else
			{
				vote = new Vote
				{
					PostId = Guid.Parse(postId),
					AuthorId = Guid.Parse(userId),
					Type = VoteType.UpVote,
					CreatedOn = DateTime.UtcNow,
				};

				await this.dbContext.Votes.AddAsync(vote);
			}

			await this.dbContext.SaveChangesAsync();
		}

		public int GetVotes(string postId)
			=> this.dbContext.Votes
			.Where(v => v.PostId.ToString() == postId)
			.Sum(v => (int)v.Type);

		private Vote? GetVote(string postId, string userId)
			=> this.dbContext.Votes
			.FirstOrDefault(v => v.PostId.ToString() == postId 
							&& v.AuthorId.ToString() == userId);
	}
}
