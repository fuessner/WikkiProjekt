using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikkiDBBlib.Models.ViewModels;
using WikkiDBBlib.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace WikkiProjekt.Helpers
{
    public class AppHelper
    {
        // Umwandeln von ViewModel in Liste von ViewModel
        // VM = ViewModel
        public static List<PersonStadtVM> GetListPersonStadtVM_from_ListPersonStadt(IEnumerable<Person> iPersonList)
        {
            // Geändert in Video 62
            var ListPersonStadtVM = new List<PersonStadtVM>();
            // Das funktioniert nicht, da in einem IEnumeralbe kein ADD funktioniert
            // IEnumerable<PersonStadtVM> listPersonStadtVM = new List<PersonStadtVM>();

            if (iPersonList != null)
            { 
                foreach(var person in iPersonList)
                {
                    var PersonStadtVM = new PersonStadtVM()
                    {
                        PID = person.PID,
                        PName = person.PName,
                        PVorname = person.PVorname,
                        PInfiziert = person.PInfiziert, 
                        PTestAbgeschlosen = person.PTestAbgeschlosen,   
                        PBild = person.PBild,
                        SID = person.SID,
                        SName = (person.Stadt is not null) ? person.Stadt.SName : String.Empty

                    };
                    ListPersonStadtVM.Add(PersonStadtVM);
                   

                }
            }

            return ListPersonStadtVM;
            // Mann könnte aber auch direkt in den Rückgabewerte eine "where" anweisung per LINQ schreiben
            // Dies würde dann wie folgt aussehen:
            // return ListPersonStadtVM.Where(p=>p.SName.Contains("SuchString")).ToList(); 
            // Ich kann auch sagen nehme nur 2 aus der Liste für den Rückgabewert
            // return ListPersonStadtVM.Take(3).ToList();
            // Kann die Liste aber auch gleich sortieren
            // return ListPersonStadtVM.OrderByDescending(p => p.PName).ToList();
        }

    }
}
