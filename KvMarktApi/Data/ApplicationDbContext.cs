using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KvMarktApi.Models;

namespace KvMarktApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Contributor> Contributor { get; set; }
        public DbSet<Scheme> Schemes { get; set; }
        public DbSet<ContributorFavoriteScheme> ContributorFavoriteScheme { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // builder.Entity<Microsoft.AspNetCore.Identity.IdentityUser>().ToTable("user").Property(p => p.Id).HasColumnName("id");
            // builder.Entity<ApplicationUser>().ToTable("MyUsers").Property(p => p.Id).HasColumnName("UserId");
            // builder.Entity<IdentityUserRole>().ToTable("MyUserRoles");
            // builder.Entity<IdentityUserLogin>().ToTable("MyUserLogins");
            // builder.Entity<IdentityUserClaim>().ToTable("MyUserClaims");
            // builder.Entity<IdentityRole>().ToTable("MyRoles");

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<ContributorFavoriteScheme>()
                .HasKey(x => new { x.SchemeId, x.ContributorId });
                // .HasKey(x => x.Id);

            builder.Entity<ContributorFavoriteScheme>()
                .HasOne(x => x.Scheme)
                .WithMany(y => y.ContributorFavoriteSchemes)
                .HasForeignKey(y => y.SchemeId);

            builder.Entity<ContributorFavoriteScheme>()
                .HasOne(x => x.Contributor)
                .WithMany(y => y.ContributorFavoriteSchemes)
                .HasForeignKey(y => y.ContributorId);



            builder.Entity<ContributorTeam>()
                // .HasKey(x => new { x.TeamId, x.ContributorId });
                .HasKey(x => x.Id);

            builder.Entity<ContributorTeam>()
                .HasOne(x => x.Team)
                .WithMany(y => y.ContributorTeams)
                .HasForeignKey(y => y.TeamId);

            builder.Entity<ContributorTeam>()
                .HasOne(x => x.Contributor)
                .WithMany(y => y.ContributorTeams)
                .HasForeignKey(y => y.ContributorId);

        }
    }
}
