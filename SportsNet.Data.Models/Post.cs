namespace SportsNet.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using SportsNet.Data.Common;
    using SportsNet.Data.Models.Enums;

    using static SportsNet.Common.EntityValidationConstants.Post;

    public class Post : BaseDeletableModel<int>
    {
        public Post()
        {
            this.Comments = new HashSet<Comment>();
            this.Votes = new HashSet<Vote>();
        }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; } = null!;

        [Required]
        public PostType Type { get; set; }

        public Guid AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; } = null!;

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Vote> Votes { get; set; }
    }
}
