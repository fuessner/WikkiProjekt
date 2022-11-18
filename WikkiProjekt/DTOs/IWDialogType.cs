using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikkiProjekt.DTOs
{

    // DTO steht für Data Transfer Objekt
    // IW steht für Info Window Dialg Type
    // Hier haben wir eine Klasse drin erstellt.
    // Diese wandeln wir in einem Enumarot um
    public enum IWDialogType
    {
        // Diese vier sachen wollen wir ausgeben
        Information, Confirmation, Textangabe, Warnung 
    }
}
