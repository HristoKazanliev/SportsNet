namespace SportsNet.Services.Tests
{
    using Microsoft.EntityFrameworkCore;

    using SportsNet.Data;
    using SportsNet.Services.Data.Interfaces;
    using SportsNet.Services.Data;
    using SportsNet.Web.ViewModels.Image;

    using static SportsNet.Services.Tests.DatabaseSeeder;

    public class ImageServiceTests
    {
        private DbContextOptions<SportsNetDbContext> dbOptions;
        private SportsNetDbContext dbContext;

        private ImageService imageService;

        [SetUp]
        public void Setup()
        {
            this.dbOptions = new DbContextOptionsBuilder<SportsNetDbContext>()
                .UseInMemoryDatabase("SportsNetInMemoryDb" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new SportsNetDbContext(this.dbOptions);

            this.dbContext.Database.EnsureCreated();
            SeedDatabase(this.dbContext);

            this.imageService = new ImageService(dbContext);
        }

        [Test]
        public async Task ImageGetImageShouldWorkCorrectly()
        {
            //Arrange
            
            //Act
            var result = this.imageService.GetImage(Image1.Id);

            //Assert
            Assert.AreEqual(1, result.Id);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}
