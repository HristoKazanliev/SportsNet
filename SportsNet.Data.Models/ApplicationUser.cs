namespace SportsNet.Data.Models
{
    using System;
    using Microsoft.AspNetCore.Identity;

    using SportsNet.Data.Common;

    public class ApplicationUser : IdentityUser<Guid>, IAuditInfo
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.Posts = new HashSet<Post>();
            this.Comments = new HashSet<Comment>();
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
