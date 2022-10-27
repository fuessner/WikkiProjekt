using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WikkiDBBlib.Models
{
    // Tabelle für Personendaten

    // System.ComponentModel.DataAnnotations hier ist die definition für die Datenbank
    // https://www.entityframeworktutorial.net/code-first/key-dataannotations-attribute-in-code-first.aspx
    public class Person

    {

        // Datenbank Definition
        // Readme beachten da man das in der Console Hochladen kann
        [Key]
        public int PID { get; set; } 
        [Required]
        public string PName { get; set; } = String.Empty;
        [Required]
        public string PVorname { get; set; } = String.Empty;
        [Required]
        public bool PInfiziert { get; set; }
        [Required]
        public bool PTestAbgeschlosen { get; set; }
        [Required]
        public Byte[]? PBild { get; set; }


        [Required]
        public int SID { get; set; }
        [ForeignKey(nameof(SID))]
        public Stadt? Stadt { get; set; }   

    }
}
