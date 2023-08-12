namespace SportsNet.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using SportsNet.Data.Models;

    public class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
                .Property(c => c.CreatedOn)
                 .HasDefaultValueSql("GETDATE()");

            builder
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(c => c.Author)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(GenerateComments());
        }

        private Comment[] GenerateComments()
        {
            ICollection<Comment> comments = new HashSet<Comment>();

            Comment comment;

            comment = new Comment()
            {
                Id = 1,
                Content = "test comment",
                AuthorId = Guid.Parse("9E0898D3-B83D-4583-B356-9D0C363EB67C"),
                PostId = Guid.Parse("9ADECFEC-0A09-4ACA-9738-7AA9E4F478D0"),                
            };
            comments.Add(comment);

            return comments.ToArray();
        }
    }
}
