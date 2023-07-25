namespace SportsNet.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;

    public class PostEntityConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder
                .Property(c => c.CreatedOn)
                 .HasDefaultValueSql("GETDATE()");

            builder
                .HasOne(p => p.Author)
                .WithMany(a => a.Posts)
                .HasForeignKey(p => p.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.Category)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(GeneratePosts());
        }

        public Post[] GeneratePosts()
        {
            ICollection<Post> posts = new HashSet<Post>();

            Post post;

            post = new Post()
            {
                Title = "Kylian Mbappe: PSG grant forward permission to speak to Al Hilal",
                Content = "Paris Saint-Germain have granted permission for Kylian Mbappe to speak to Al Hilal after the Saudi club's world-record £259m bid.",
                Type = Models.Enums.PostType.Media,
                AuthorId = Guid.Parse("9E0898D3-B83D-4583-B356-9D0C363EB67C"),
                CategoryId = 1,
            };
            posts.Add(post);

            post = new Post()
            {
                Title = "Lando breaks Max's trophy!",
                Content = "The impact of Norris' traditional celebration of smashing the champagne bottle on the ground to spray the champagne accidentally saw Verstappen's trophy fall over and break.",
                Type = Models.Enums.PostType.Humour,
                AuthorId = Guid.Parse("9E0898D3-B83D-4583-B356-9D0C363EB67C"),
                CategoryId = 3,
            };
            posts.Add(post);

            post = new Post()
            {
                Title = "Wimbledon men's final: Carlos Alcaraz defeats seven-time champion Novak Djokovic",
                Content = "World No 1 Carlos Alcaraz ended Novak Djokovic's hopes of a record-equalling 24th Grand Slam to claim his maiden Wimbledon title in a five-set epic, 1-6 7-6 (8-6) 6-1 3-6 6-4.",
                Type = Models.Enums.PostType.Media,
                AuthorId = Guid.Parse("9E0898D3-B83D-4583-B356-9D0C363EB67C"),
                CategoryId = 2,
            };
            posts.Add(post);

            return posts.ToArray();
        }
    }
}
