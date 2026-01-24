using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalApp.Data.Models
{
    // Join table for Many-to-Many between JournalEntry and Tag
    public class JournalEntryTag
    {
        public int JournalEntryId { get; set; }
        public JournalEntry JournalEntry { get; set; } = null!;

        public int TagId { get; set; }
        public Tag Tag { get; set; } = null!;
    }
}

