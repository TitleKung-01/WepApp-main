using api;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext : IdentityDbContext<
    AppUser,
    AppRole,
    int,
    IdentityUserClaim<int>,
    AppUserRole,
    IdentityUserLogin<int>,
    IdentityRoleClaim<int>,
    IdentityUserToken<int>
>
{
    public DbSet<UserLike> Likes { get; set; }
    public DbSet<Message> Messages { get; set; }

    // public DbSet<AppUser> Users { get; set; }
    public DataContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<AppUser>()
            .HasMany(appUser => appUser.UserRoles)
            .WithOne(appUserRole => appUserRole.User)
            .HasForeignKey(appUserRole => appUserRole.UserId)
            .IsRequired();

        modelBuilder.Entity<AppRole>()
            .HasMany(appRole => appRole.UserRoles)
            .WithOne(appUserRole => appUserRole.Role)
            .HasForeignKey(appUserRole => appUserRole.RoleId)
            .IsRequired();


        modelBuilder.Entity<UserLike>()
            .HasOne(userlike => userlike.SourceUser)
            .WithMany(appuser => appuser.LikedUsers)
            .HasForeignKey(userlike => userlike.SourceUserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserLike>()
            .HasOne(userlike => userlike.LikedUser)
            .WithMany(appuser => appuser.LikedByUsers)
            .HasForeignKey(userlike => userlike.LikedUserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Message>()
            .HasOne(message => message.Recipient)
            .WithMany(appuser => appuser.MessagesReceived)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Message>()
            .HasOne(message => message.Sender)
            .WithMany(appuser => appuser.MessagesSent)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserLike>().HasKey(pk => new { pk.SourceUserId, pk.LikedUserId });
    }


}