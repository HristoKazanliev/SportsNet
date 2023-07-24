namespace SportsNet.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(GenerateCategories());
        }

        private Category[] GenerateCategories()
        {
            ICollection<Category> categories = new HashSet<Category>(); 

            Category category;

            category = new Category()
            {
                Name = "Football",
                Description = "Live games, scores, latest news, transfers, results, fixtures and team news",
                ImageUrl = "https://static.standard.co.uk/2023/07/24/11/pltransfer240723v2a.jpg?crop=3%3A2%2Csmart&width=640&auto=webp&quality=75"
            };
            categories.Add(category);

            category = new Category()
            {
                Name = "Tennis",
                Description = "Tennis Live Scores, News, Videos, Player Rankings",
                ImageUrl = "https://s.yimg.com/uu/api/res/1.2/c8QVJhoxt_hJhBExOhzFlg--~B/Zmk9ZmlsbDtoPTUwNTtweW9mZj0wO3E9OTU7dz05MDA7YXBwaWQ9eXRhY2h5b24-/https://s.yimg.com/os/creatr-uploaded-images/2023-07/89df8d30-2410-11ee-bddf-4f5a96592784"
            };
            categories.Add(category);

            category = new Category()
            {
                Name = "Formula 1",
                Description = "Enter the world of Formula 1. Latest news, videos, standings and results.",
                ImageUrl = "https://media.gettyimages.com/id/1435986123/pt/foto/second-placed-lewis-hamilton-of-great-britain-and-mercedes-race-winner-max-verstappen-of-the.jpg?s=612x612&w=gi&k=20&c=rs3Y1m05usX77zySzb2WJSA9s7JrM_rSeRW-lBgkMWM="
            };
            categories.Add(category);

            return categories.ToArray();
        }
    }
}
