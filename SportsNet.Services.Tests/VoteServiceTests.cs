namespace SportsNet.Services.Tests
{
    using Microsoft.EntityFrameworkCore;

    using SportsNet.Data;
    using SportsNet.Services.Data;
    using SportsNet.Services.Data.Interfaces;
    using static SportsNet.Services.Tests.DatabaseSeeder;

    public class VoteServiceTests
    {
        private DbContextOptions<SportsNetDbContext> dbOptions;
        private SportsNetDbContext dbContext;

        private IVoteService voteService;

        [SetUp]
        public void Setup()
        {
            this.dbOptions = new DbContextOptionsBuilder<SportsNetDbContext>()
                .UseInMemoryDatabase("SportsNetInMemoryDb" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new SportsNetDbContext(this.dbOptions);
            
            this.dbContext.Database.EnsureCreated();
            SeedDatabase(this.dbContext);

            this.voteService = new VoteService(dbContext);
        }

        //[Test]
        //public async Task VoteVoteAsyncShouldWorkWithCorrectPostId()
        //{
        //    //Act
        //    await this.voteService.VoteAsync(Post2.Id.ToString(), User1.Id.ToString());

        //    //Assert
        //    Assert.True(this.dbContext.Votes.Count() == Votes.Count);
        //}

        [Test]
        public async Task VoteGetVotesShouldWorkWithCorrectPostId()
        {
            //Arrange

            //Act
            var result = this.voteService.GetVotes(Post2.Id.ToString());

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}
