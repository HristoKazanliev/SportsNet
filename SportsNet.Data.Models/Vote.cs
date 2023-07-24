namespace SportsNet.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using SportsNet.Data.Common;
    using SportsNet.Data.Models.Enums;

    public class Vote : BaseDeletableModel<int>
    {
        [Required]
        public VoteType Type { get; set; }

        public Guid AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; } = null!;

        public int PostId { get; set; }

        public virtual Post Post { get; set; } = null!;
    }
}
