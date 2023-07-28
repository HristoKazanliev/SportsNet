namespace SportsNet.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .Property(c => c.CreatedOn)
                 .HasDefaultValueSql("GETDATE()");

            builder.HasData(GenerateCategories());
        }

        private Category[] GenerateCategories()
        {
            ICollection<Category> categories = new HashSet<Category>(); 

            Category category;

            category = new Category()
            {
                Id = 1,
                Name = "Football",
                Description = "Live games, scores, latest news, transfers, results, fixtures and team news",
                ImageUrl = "https://static.standard.co.uk/2023/07/24/11/pltransfer240723v2a.jpg?crop=3%3A2%2Csmart&width=640&auto=webp&quality=75"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 2,
                Name = "Volleyball ",
                Description = "Stay current on the Tournaments, leagues, teams and players news, scores, stats, standings",
                ImageUrl = "https://epaimages.com/downloadpicturepreview.pp?pictureid=11615719"
			};
            categories.Add(category);

            category = new Category()
            {
                Id = 3,
                Name = "Formula 1",
                Description = "Enter the world of Formula 1. Latest news, videos, standings and results.",
                ImageUrl = "https://media.gettyimages.com/id/1435986123/pt/foto/second-placed-lewis-hamilton-of-great-britain-and-mercedes-race-winner-max-verstappen-of-the.jpg?s=612x612&w=gi&k=20&c=rs3Y1m05usX77zySzb2WJSA9s7JrM_rSeRW-lBgkMWM="
            };
            categories.Add(category);

            return categories.ToArray();
        }
    }
}
