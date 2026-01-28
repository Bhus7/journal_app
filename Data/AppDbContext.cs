using Microsoft.EntityFrameworkCore;
using JournalApp.Data.Models;

namespace JournalApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<JournalEntry> JournalEntries => Set<JournalEntry>();
        public DbSet<AppUser> Users => Set<AppUser>();

        // Tags
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<JournalEntryTag> JournalEntryTags => Set<JournalEntryTag>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Dictionary<string, object>
            modelBuilder.Entity<JournalEntry>()
                .HasIndex(e => e.EntryDate); // Not unique anymore to allow multiple entries per day

            modelBuilder.Entity<JournalEntry>()
                .Property(e => e.PrimaryMood)
                .IsRequired();

            // AppUser
            modelBuilder.Entity<AppUser>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<AppUser>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Tag name must be unique
            modelBuilder.Entity<Tag>()
                .HasIndex(t => t.Name)
                .IsUnique();

            // Many-to-Many: JournalEntry <-> Tag via JournalEntryTag
            modelBuilder.Entity<JournalEntryTag>()
                .HasKey(jt => new { jt.JournalEntryId, jt.TagId });

            modelBuilder.Entity<JournalEntryTag>()
                .HasOne(jt => jt.JournalEntry)
                .WithMany(e => e.JournalEntryTags)
                .HasForeignKey(jt => jt.JournalEntryId);

            modelBuilder.Entity<JournalEntryTag>()
                .HasOne(jt => jt.Tag)
                .WithMany(t => t.JournalEntryTags)
                .HasForeignKey(jt => jt.TagId);

            // Pre-built tags
            modelBuilder.Entity<Tag>().HasData(
                new Tag { Id = 1, Name = "Work" },
                new Tag { Id = 2, Name = "Study" },
                new Tag { Id = 3, Name = "Health" },
                new Tag { Id = 4, Name = "Family" },
                new Tag { Id = 5, Name = "Friends" },
                new Tag { Id = 6, Name = "Fitness" },
                new Tag { Id = 7, Name = "Finance" },
                new Tag { Id = 8, Name = "Travel" }
            );
        }
    }
}
