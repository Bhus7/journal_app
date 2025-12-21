using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JournalApp.Data.Models;
namespace JournalApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<JournalEntry> JournalEntries => Set<JournalEntry>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<AppUser> Users => Set<AppUser>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JournalEntry>()
                .HasIndex(e => e.EntryDate)
                .IsUnique();

            modelBuilder.Entity<JournalEntry>()
                .Property(e => e.PrimaryMood)
                .IsRequired();

            modelBuilder.Entity<AppUser>()
                .HasKey(u => u.Id);
                
            modelBuilder.Entity<AppUser>()
                .HasIndex(u => u.Username)
                .IsUnique();
                
        }

    }
}
