namespace SportsNet.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using SportsNet.Data.Models;

    public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
               .Property(c => c.CreatedOn)
                .HasDefaultValueSql("GETDATE()");

            builder.HasData(GenerateUsers());
        }

        private ApplicationUser[] GenerateUsers()
        {
            ICollection<ApplicationUser> users = new HashSet<ApplicationUser>();

            ApplicationUser user;

            user = new ApplicationUser()
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
            users.Add(user);

            user = new ApplicationUser()
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
            users.Add(user);

            return users.ToArray();
        }
    }
}
