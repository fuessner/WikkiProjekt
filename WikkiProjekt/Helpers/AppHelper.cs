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
        public static List<PersonStadtVM> GetListPersonStadtVML_from_ListPersonStadtL(IEnumerable<Person> iPersonList)
        {
            var ListPersonStadtVM = new List<PersonStadtVM>();

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
                        PBild = person.PBild
                    };
                    ListPersonStadtVM.Add(PersonStadtVM);
                }
            }

            return ListPersonStadtVM;

        }

    }
}
