using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WikkiDBBlib.Models.ViewModels
{
    public class PersonStadtVM
    {

        // DB Properties
        public int PID { get; set; }     
        public string PName { get; set; } = String.Empty;     
        public string PVorname { get; set; } = String.Empty;       
        public bool PInfiziert { get; set; }     
        public bool PTestAbgeschlosen { get; set; }      
        public Byte[]? PBild { get; set; }      
        public int SID { get; set; }
        public String SName { get; set; } = String.Empty;

        // DB Helper Propteries
        // PROP und zwei mal TAB drücken

        // Wir setzen hier die Variable PNichtInfiziert
        // !Pinfiziert ist das gegenteil von Infiziert

        public string PVornameName
        {
            get
            {
                return $"{PVorname} {PName}";
            }
        }
        // Alternativ zu PVornameName:
        // Damit spare ich mir den get. Geht aber nur wenn ein GET vorhanden ist. Sobald SET benötigt wird geht das nicht
        public string PVornameNameGesamt => $"{PVorname} {PName}";

        public bool PNichtInfiziert
        {
            get
            {
                return !PInfiziert;
            }
            set
            {
                PInfiziert = !value;
            }
        }
        public bool PTestNichtAbgeschlossen { 
                    get { 
                        return !PTestAbgeschlosen;
                        }
                    set {
                        PTestAbgeschlosen = !value;
                        } 
        }

        // Da hier nur ein GETer verwendet wird benutze ich =>
        public BitmapImage PBitmapImage => _GetBitmapImage(PBild);

        // Helber Funktionen
        private BitmapImage _GetBitmapImage(byte[]? iBildByte)
        { 
            var BitmapImg = new BitmapImage();
            if (iBildByte != null || iBildByte?.Length != 0)
                { 
                    using(var stream = new MemoryStream())
                        { 

                        }
                }
            return BitmapImg;
        }
    }
}
