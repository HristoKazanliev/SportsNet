namespace SportsNet.Services.Data
{
	using System.Threading.Tasks;

	using SportsNet.Data.Models;
	using SportsNet.Data.Models.Enums;
	using SportsNet.Data.Repositories.Interfaces;
	using SportsNet.Services.Data.Interfaces;

	public class VoteService : IVoteService
	{
		private readonly IRepository<Vote> voteRepository;

        public VoteService(IRepository<Vote> voteRepository)
        {
            this.voteRepository = voteRepository;
        }

		public async Task VoteAsync(string postId, string userId)
		{
			Vote? vote = this.GetVote(postId, userId);
			if (vote != null) 
			{ 
				vote.Type = VoteType.UpVote;
			}
			else
			{
				vote = new Vote
				{
					PostId = Guid.Parse(postId),
					AuthorId = Guid.Parse(userId),
					Type = VoteType.UpVote,
					CreatedOn = DateTime.UtcNow.AddHours(3),
				};

				await this.voteRepository.AddAsync(vote);
			}

			await this.voteRepository.SaveChangesAsync();
		}

		public int GetVotes(string postId)
			=> this.voteRepository.All()
			.Where(v => v.PostId.ToString() == postId)
			.Sum(v => (int)v.Type);

		private Vote? GetVote(string postId, string userId)
			=> this.voteRepository.All()
			.FirstOrDefault(v => v.PostId.ToString() == postId 
							&& v.AuthorId.ToString() == userId);
	}
}
