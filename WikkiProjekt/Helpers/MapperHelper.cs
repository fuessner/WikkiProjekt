using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikkiDBBlib.Models;
using WikkiDBBlib.Models.ViewModels;

namespace WikkiProjekt.Helpers
{
    public class MapperHelper
    {
        // static nehmen wir damit wir direkt auf die Klasse zugreifen können ohne
        // die vorher kreieren zu müssen
        public static PersonStadtVM Map_PersVM_to_PersVM(PersonStadtVM iPersonStadtVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PersonStadtVM, PersonStadtVM>());
            var mapper = new Mapper(config);

            var PersonStadtVM = mapper.Map<PersonStadtVM, PersonStadtVM>(iPersonStadtVM);
            return PersonStadtVM;

        }

        public static Person Map_PersVM_To_Pers(PersonStadtVM iPersonStadtVM)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<PersonStadtVM, Person>());
            var mapper = new Mapper(config);

            return mapper.Map<PersonStadtVM, Person>(iPersonStadtVM);


        }

    }
}
