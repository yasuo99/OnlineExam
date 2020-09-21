using LuyenThiOnline.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace LuyenThiOnline
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountBadge> AccountBadges { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<AccountCertificate> AccountCertificates { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            builder.Entity<AccountBadge>().HasKey(key => new { key.AccountId, key.BadgeId });
            builder.Entity<AccountBadge>().HasOne(u => u.Account).WithMany(u => u.Badges).HasForeignKey(u => u.AccountId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<AccountBadge>().HasOne(u => u.Badge)
            .WithMany(u => u.Accounts)
            .HasForeignKey(u => u.BadgeId)
            .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Enrolled>().HasKey(key => new { key.SubjectId, key.AccountId });
            builder.Entity<AccountCertificate>().HasKey(key => new {key.AccountId,key.CertificateId});
        }
    }
}
