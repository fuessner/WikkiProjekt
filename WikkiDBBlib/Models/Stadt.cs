using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikkiDBBlib.Models
{
    // Hier wird die Datebank für die Stadt definiert
    // SID = Stadt ID       = Key in der DB
    // SName = Stadt Name   = Pflichtfeld
    public class Stadt
    {
        [Key]
        public int SID { get; set; }
        [Required]
        public String SName { get; set; } = String.Empty;
    }
}
