using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikkiDBBlib.Models;

namespace WikkiDBBlib.DBAccess
{
    public class DBUnit
    {
        // In dieser Klasse will ich verhinden, das ich immer mit NEW eine neue Klasse
        // erzeugen muss
        // Static muss sein, damit das nicht immer neu declaiert werden muss.

        public static DBAccessHelper<Person> Person
        {
            get
            {
                return new DBAccessHelper<Person>();
            }
        }
        public static DBAccessHelper<Stadt> Stadt
        {
            get
            {
                return new DBAccessHelper<Stadt>();
            }
        }
    }
}
