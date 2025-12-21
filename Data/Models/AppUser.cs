using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JournalApp.Data.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Username { get; set; } = "Owner";
        public string PinHash { get; set; } = "";
        public string Salt { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
