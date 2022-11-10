using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikkiDBBlib.Models.ViewModels
{
    public class PersonStadtVM
    {
        public int PID { get; set; }     
        public string PName { get; set; } = String.Empty;     
        public string PVorname { get; set; } = String.Empty;       
        public bool PInfiziert { get; set; }     
        public bool PTestAbgeschlosen { get; set; }      
        public Byte[]? PBild { get; set; }      
        public int SID { get; set; }
        public String SName { get; set; } = String.Empty;

    }
}
