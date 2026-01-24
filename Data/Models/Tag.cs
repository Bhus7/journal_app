using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalApp.Data.Models
{
    public class Tag
    {
        public int Id { get; set; }

        // e.g. "Work", "Health", "Study"
        public string Name { get; set; } = "";

        // Navigation
        public ICollection<JournalEntryTag> JournalEntryTags { get; set; } = new List<JournalEntryTag>();
    }
}

