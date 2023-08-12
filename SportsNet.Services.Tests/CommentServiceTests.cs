namespace SportsNet.Services.Tests
{
    using Microsoft.EntityFrameworkCore;

    using SportsNet.Data;
    using SportsNet.Data.Models;
    using SportsNet.Services.Data;
    using SportsNet.Services.Data.Interfaces;

    using static SportsNet.Services.Tests.DatabaseSeeder;

    public class CommentServiceTests
    {
        private DbContextOptions<SportsNetDbContext> dbOptions;
        private SportsNetDbContext dbContext;

        private ICommentService commentService;

        [SetUp]
        public void Setup()
        {
            this.dbOptions = new DbContextOptionsBuilder<SportsNetDbContext>()
                .UseInMemoryDatabase("SportsNetInMemoryDb" + Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;
            this.dbContext = new SportsNetDbContext(this.dbOptions);

            this.dbContext.Database.EnsureCreated();
            SeedDatabase(this.dbContext);

            this.commentService = new CommentService(dbContext);
        }

        [Test]
        public async Task CommentCreateShouldWorkWithCorrectPostId()
        {
            //Arrange
            
            //Act
            await this.commentService.CreateCommentAsync("9ADECFEC-0A09-4ACA-9738-7AA9E4F478D0", "9E0898D3-B83D-4583-B356-9D0C363EB67C", Comment1.Content);

            //Assert
            Assert.True(this.dbContext.Comments.Count() != 0);
        }

        [Test]
        public async Task CommentDeleteShouldWorkCorrectly()
        {
            await this.commentService.DeleteCommentAsync("9ADECFEC-0A09-4ACA-9738-7AA9E4F478D0", 1);

            Assert.True(this.dbContext.Comments.Count() == 0);
        }

        [Test]
        public async Task CommentGetForDeleteByIdAsyncShouldWorkCorrectly()
        {
            var result = await this.commentService.GetCommentForDeleteByIdAsync("9ADECFEC-0A09-4ACA-9738-7AA9E4F478D0", 1);

            Assert.AreEqual(Comment1.Content, result.Content);
        }


        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

    }
}
