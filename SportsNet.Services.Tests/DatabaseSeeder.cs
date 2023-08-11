namespace SportsNet.Services.Tests
{
	using SportsNet.Data;
	using SportsNet.Data.Models;
	using SportsNet.Data.Models.Enums;

	public static class DatabaseSeeder
	{
		public static Post Post1 = null!;
		public static Post Post2 = null!;
		public static Vote Vote1 = null!;
		public static Vote Vote2 = null!;
		public static Image Image1 = null!;
		public static Image Image2 = null!;
		public static Comment Comment1 = null!;
		public static Comment Comment2 = null!;
		public static Category Category1 = null!;
		public static Category Category2 = null!;
		public static ApplicationUser User1 = null!;
		public static ApplicationUser User2 = null!;
		public static ApplicationUser User3 = null!;

		public static void SeedDatabase(SportsNetDbContext dbContext)
		{
			User1 = new ApplicationUser()
			{
				Id = Guid.Parse("9E0898D3-B83D-4583-B356-9D0C363EB67C"),
				UserName = "user@user.com",
				NormalizedUserName = "USER@USER.COM",
				Email = "user@user.com",
				NormalizedEmail = "USER@USER.COM",
				EmailConfirmed = false,
				PasswordHash = "AQAAAAEAACcQAAAAEI1UrX+8le4jLxQkuFHOBUqUu4KJrtNHT0IIghd8tPveAVhw8WyKB/mcvZ9fxAdDOg==",
				SecurityStamp = "I3TXAEQHM4VVFE3OCF63HEM4QMCUPLV7",
				ConcurrencyStamp = "509b97ff-8d3d-43c1-ac86-0ae4888bd860",
				PhoneNumber = null,
				PhoneNumberConfirmed = false,
				TwoFactorEnabled = false,
				LockoutEnd = null,
				LockoutEnabled = true,
				AccessFailedCount = 0
			};
			User2 = new ApplicationUser()
			{
				Id = Guid.Parse("325389F8-4E8D-4E70-8AA3-0527F0746E54"),
				UserName = "admin@admin.com",
				NormalizedUserName = "ADMIN@ADMIN.COM",
				Email = "admin@admin.com",
				NormalizedEmail = "ADMIN@ADMIN.COM",
				EmailConfirmed = false,
				PasswordHash = "AQAAAAEAACcQAAAAEI1UrX+8le4jLxQkuFHOBUqUu4KJrtNHT0IIghd8tPveAVhw8WyKB/mcvZ9fxAdDOg==",
				SecurityStamp = "R3UYREJWAJ6WK4XZQA7J4CTVQLSHUR62",
				ConcurrencyStamp = "25fe56ef-27e4-4320-8857-ebab667c7230",
				PhoneNumber = null,
				PhoneNumberConfirmed = false,
				TwoFactorEnabled = false,
				LockoutEnd = null,
				LockoutEnabled = true,
				AccessFailedCount = 0
			};
			Category1 = new Category()
			{
				Id = 1,
				Name = "Football",
				Description = "Live games, scores, latest news, transfers, results, fixtures and team news",
				ImageUrl = "https://static.standard.co.uk/2023/07/24/11/pltransfer240723v2a.jpg?crop=3%3A2%2Csmart&width=640&auto=webp&quality=75",
				CreatedOn = DateTime.UtcNow
			};
			Category2 = new Category()
			{
				Id = 2,
				Name = "Formula 1",
				Description = "Enter the world of Formula 1. Latest news, videos, standings and results.",
				ImageUrl = "https://media.gettyimages.com/id/1435986123/pt/foto/second-placed-lewis-hamilton-of-great-britain-and-mercedes-race-winner-max-verstappen-of-the.jpg?s=612x612&w=gi&k=20&c=rs3Y1m05usX77zySzb2WJSA9s7JrM_rSeRW-lBgkMWM=",
				CreatedOn = DateTime.UtcNow
			};
			Image1 = new Image()
			{
				Id = 1,
				Description = "Botev new stadium",
				ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/35/Stadium_Hristo_Botev.jpg/640px-Stadium_Hristo_Botev.jpg",
				AuthorId = Guid.Parse("9E0898D3-B83D-4583-B356-9D0C363EB67C"),
				CreatedOn = DateTime.UtcNow,
				IsApproved = true
			};
			Image2 = new Image()
			{
				Id= 2,
				Description = "Chelsea stadium",
				ImageUrl = "https://preview.redd.it/some-pics-from-stamford-bridge-stadium-tour-v0-1bksjrthhbj91.jpg?width=640&crop=smart&auto=webp&s=9ee7442f375e49324926464ec71cd71fd468be46",
				AuthorId = Guid.Parse("9E0898D3-B83D-4583-B356-9D0C363EB67C"),
				CreatedOn = DateTime.UtcNow,
				IsApproved = false
			};
			Post1 = new Post()
			{
				Id = Guid.Parse("9ADECFEC-0A09-4ACA-9738-7AA9E4F478D0"),
				Title = "Kylian Mbappe: PSG grant forward permission to speak to Al Hilal",
				Content = "Paris Saint-Germain have granted permission for Kylian Mbappe to speak to Al Hilal after the Saudi club's world-record £259m bid.",
				Type = PostType.Media,
				AuthorId = Guid.Parse("9E0898D3-B83D-4583-B356-9D0C363EB67C"),
				CategoryId = 1,
				CreatedOn = DateTime.UtcNow,
			};
			Post2 = new Post()
			{
				Id = Guid.Parse("63066625-0922-42F3-8798-F132295079E3"),
				Title = "Lando breaks Max's trophy!",
				Content = "The impact of Norris' traditional celebration of smashing the champagne bottle on the ground to spray the champagne accidentally saw Verstappen's trophy fall over and break.",
				Type = PostType.Humour,
				AuthorId = Guid.Parse("325389F8-4E8D-4E70-8AA3-0527F0746E54"),
				CategoryId = 2,
				CreatedOn = DateTime.UtcNow
			};
			Vote1 = new Vote()
			{
				Id = 1,
				Type = VoteType.UpVote,
				AuthorId = Guid.Parse("9E0898D3-B83D-4583-B356-9D0C363EB67C"),
				PostId = Guid.Parse("9ADECFEC-0A09-4ACA-9738-7AA9E4F478D0")
			};
			Vote2 = new Vote()
			{
				Id = 2,
				Type = VoteType.UpVote,
				AuthorId = Guid.Parse("325389F8-4E8D-4E70-8AA3-0527F0746E54"),
				PostId = Guid.Parse("9ADECFEC-0A09-4ACA-9738-7AA9E4F478D0")
			};
			Comment1 = new Comment()
			{
				Id = 1,
				Content = "test comment",
				AuthorId = Guid.Parse("9E0898D3-B83D-4583-B356-9D0C363EB67C"),
				PostId = Guid.Parse("9ADECFEC-0A09-4ACA-9738-7AA9E4F478D0")
			};
			Comment2 = new Comment()
			{
				Id = 2,
				Content = "testing second comment",
				AuthorId = Guid.Parse("325389F8-4E8D-4E70-8AA3-0527F0746E54"),
				PostId = Guid.Parse("63066625-0922-42F3-8798-F132295079E3")
			};

			dbContext.Users.Add(User1);
			dbContext.Users.Add(User2);
			dbContext.Categories.AddRange(Category1, Category2);
			dbContext.Images.AddRange(Image1, Image2);
			dbContext.Posts.AddRange(Post1, Post2);
			dbContext.Votes.AddRange(Vote1, Vote2);
			dbContext.Comments.AddRange(Comment1, Comment2);
		}
	}
}
