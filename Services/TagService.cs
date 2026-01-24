using JournalApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JournalApp.Data;
using Microsoft.EntityFrameworkCore;


namespace JournalApp.Services
{
    public class TagService
    {
        private readonly AppDbContext _db;

        public TagService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Tag>> GetAllTagsAsync()
        {
            return await _db.Tags
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<Tag> GetOrCreateTagAsync(string name)
        {
            name = (name ?? "").Trim();
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tag name cannot be empty.");

            // Normalize (prevents duplicates like "work" and "Work")
            var normalized = char.ToUpper(name[0]) + name.Substring(1).ToLower();

            var existing = await _db.Tags.FirstOrDefaultAsync(t => t.Name == normalized);
            if (existing != null) return existing;

            var tag = new Tag { Name = normalized };
            _db.Tags.Add(tag);
            await _db.SaveChangesAsync();
            return tag;
        }

        public async Task<List<int>> GetTagIdsForEntryAsync(int journalEntryId)
        {
            return await _db.JournalEntryTags
                .Where(x => x.JournalEntryId == journalEntryId)
                .Select(x => x.TagId)
                .ToListAsync();
        }

        public async Task SetTagsForEntryAsync(int journalEntryId, IEnumerable<int> tagIds)
        {
            var ids = tagIds.Distinct().ToList();

            var existingLinks = await _db.JournalEntryTags
                .Where(x => x.JournalEntryId == journalEntryId)
                .ToListAsync();

            _db.JournalEntryTags.RemoveRange(existingLinks);

            foreach (var id in ids)
            {
                _db.JournalEntryTags.Add(new JournalEntryTag
                {
                    JournalEntryId = journalEntryId,
                    TagId = id
                });
            }

            await _db.SaveChangesAsync();
        }
    }
}
