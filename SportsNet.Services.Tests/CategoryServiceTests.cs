namespace SportsNet.Services.Tests
{
	using Microsoft.EntityFrameworkCore;

	using SportsNet.Data;
	using SportsNet.Data.Models;
	using SportsNet.Data.Repositories.Interfaces;
	using SportsNet.Services.Data;
	using SportsNet.Services.Data.Interfaces;
    using SportsNet.Web.ViewModels.Category;

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

        [Test]
        public async Task CategoryShouldReturnTrueWhenDoesExistsByName()
        {
            //Arrange
            var categoryName = "Football";

            //Act
            var result = this.categoryService.ExistsByNameAsync(categoryName);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task CategoryShouldReturnFalseWhenDoesNotExistsByName()
        {
            //Arrange
            var categoryName = "test";

            //Act
            var result = this.categoryService.ExistsByNameAsync(categoryName);

            //Assert
            Assert.IsFalse(result);
        }
        

        [Test]
        public async Task CategoryShouldReturnTrueWithAllCategoriesAsync()
        {
            //Act
            var result = await this.categoryService.AllCategoriesAsync();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }
        

        [Test]
        public async Task CategoryShouldReturnTrueWithGetCategories()
        {
            //Act
            var result = await this.categoryService.GetCategories();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
        }

        [Test]
        public async Task CategoryShouldReturnTrueWithGetCategoryById()
        {
            //Arrange
            var categoryId = 2;

            //Act
            var result = this.categoryService.GetCategoryById(categoryId);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Id);
            Assert.AreEqual("Formula 1", result.Name);
        }

        [Test]
        public async Task CategoryShouldReturnFalseWithWrongGetCategoryById()
        {
            //Arrange
            var categoryId = 1;

            //Act
            var result = this.categoryService.GetCategoryById(categoryId);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual("Formula 1", result.Name);
        }

        [Test]
        public async Task CategoryShouldReturn1WithPostsNumber()
        {
            //Arrange
            var categoryId = 1;

            //Act
            var result = this.categoryService.GetCategoryById(categoryId);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Posts.Count);
        }


        //[Test]
        //public async Task CategoryDeleteShouldWorkCorrectly()
        //{
        //    //Arrange

        //    //Act
        //    await this.categoryService.DeleteCategoryAsync(DatabaseSeeder.Category1.Id);

        //    //Assert
        //    Assert.IsTrue(DatabaseSeeder.Category1.IsDeleted);
        //}

        //[Test]
        //public async Task CategoryCreateShouldWorkCorrectly()
        //{
        //    //Arrange
        //    var category = new Category()
        //    {
        //        Name = "Test",
        //        Description = "Testing create method with nunit",
        //        ImageUrl = "test"
        //    };

        //    //Act
        //    await this.categoryService.CreateAsync(category.Name, category.Description, category.ImageUrl);
        //    var result = this.categoryService.ExistsByNameAsync(category.Name);

        //    //Assert
        //    Assert.IsTrue(result);
        //}

        [Test]
        public async Task CategoryDetailsShouldWorkCorrectly()
        {
            //Arrange
            var categoryId = 2;
            CategoryDetailsViewModel model = new CategoryDetailsViewModel
            {
                Id = 2,
                Name = "Formula 1"
            };

            //Act
            var result = await this.categoryService.GetDetailsForName(categoryId);

            //Assert
            Assert.AreEqual(model.Id, result.Id);
            Assert.AreEqual(model.Name, result.Name);
        }

        [Test]
        public async Task CategoryEditShouldWorkCorrectly()
        {
            //Arrange
            Category category = this.categoryService.GetCategoryById(1);
            category.Name = "Test Edit Category";

            //Act
             this.categoryService.EditCategoryAsync(1, category.Name, category.Description, category.ImageUrl);

            //Assert
            Assert.AreEqual("Test Edit Category", category.Name);
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

    }
}