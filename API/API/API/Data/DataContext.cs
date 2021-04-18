using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<FriendInvitation>(b =>
            {
                b.HasKey(k => new { k.InvitedUserId, k.SourceUserId });

                b.HasOne(r => r.SourceUser)
                .WithMany(f => f.InvitedFrom)
                .HasForeignKey(o => o.SourceUserId)
                .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(r => r.InvitedUser)
                .WithMany(f => f.InvitedBy)
                .HasForeignKey(o => o.InvitedUserId)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<FriendInvitation> FriendInvitations { get; set; }
    }
}
