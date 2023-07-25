namespace SportsNet.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using SportsNet.Data.Common;

    using static SportsNet.Common.EntityValidationConstants.Comment;

    public class Comment : BaseModel<int>
    {
        public Comment()
        {
            this.Votes = new HashSet<Vote>();
        }

        [Required]
        [MaxLength(MaxLength)]
        public string Content { get; set; } = null!;

        public Guid AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; } = null!;

        public Guid PostId { get; set; }

        public virtual Post Post { get; set; } = null!;

        public virtual ICollection<Vote> Votes { get; set; }
    }
}
