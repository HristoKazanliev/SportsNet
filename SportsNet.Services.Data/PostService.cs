namespace SportsNet.Services.Data
{
    using Interfaces;
    using SportsNet.Data;

    public class PostService : IPostService
    {
        private readonly SportsNetDbContext dbContext;

        public PostService(SportsNetDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
