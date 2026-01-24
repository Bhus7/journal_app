using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalApp.Data.Models
{
    public class JournalEntry
    {
        public int Id { get; set; }

        public DateOnly EntryDate { get; set; }

        public string Title { get; set; } = "";
        public string Content { get; set; } = "";

        public string PrimaryMood { get; set; } = "";

        public string? SecondaryMood1 { get; set; }

        public String? SecondaryMood2 { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<JournalEntryTag> JournalEntryTags { get; set; } = new List<JournalEntryTag>();

    }
}
