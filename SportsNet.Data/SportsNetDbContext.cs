namespace SportsNet.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Models;

    public class SportsNetDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public SportsNetDbContext(DbContextOptions<SportsNetDbContext> options)
            : base(options)
        {
        }
    }
}