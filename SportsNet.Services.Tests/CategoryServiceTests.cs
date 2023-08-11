namespace SportsNet.Services.Tests
{
	using Microsoft.EntityFrameworkCore;
	using SportsNet.Data;
	using SportsNet.Data.Models;
	using SportsNet.Data.Repositories.Interfaces;
	using SportsNet.Services.Data;
	using SportsNet.Services.Data.Interfaces;

	using static SportsNet.Services.Tests.DatabaseSeeder;

	public class CategoryServiceTests
	{
		private DbContextOptions<SportsNetDbContext> dbOptions;
		private SportsNetDbContext dbContext;

		private ICategoryService categoryService;

		[SetUp]
		public void Setup()
		{
			this.dbOptions = new DbContextOptionsBuilder<SportsNetDbContext>()
				.UseInMemoryDatabase("SportsNetInMemoryDb" + Guid.NewGuid().ToString())
				.Options;
			this.dbContext = new SportsNetDbContext(this.dbOptions);

			this.dbContext.Database.EnsureCreated();
			SeedDatabase(this.dbContext);
			
			this.categoryService = new CategoryService(dbContext);
		}

		[Test]
		public async Task CategoryShouldReturnTrueWhenExistsById()
		{
			//Arrange
			var categoryId = 1;

			//Act
			var result = this.categoryService.ExistsByIdAsync(categoryId);

			//Assert
			Assert.IsTrue(result);
		}

        [Test]
        public async Task CategoryShouldReturnFalseWhenDoesNotExistById()
        {
            //Arrange
            var categoryId = 5;

            //Act
            var result = this.categoryService.ExistsByIdAsync(categoryId);

            //Assert
            Assert.IsFalse(result);
        }
    }
}