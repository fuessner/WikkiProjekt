using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

// Diese Klasse ist dafür gedacht um bei den Städten im Kontext Menü einen Doppelpunkt
// hinter der Stand einzutragen.
// Das wäre auch anderes gegengen über TextBox mit MultiBinding

// : IValueConverter wurde per Hand dazugefügt. Durch STRG + . wurden dann die beiden
// Objekte Convert und ConvertBack hinzugefügt.

namespace WikkiProjekt.Converters
{
    public class CityCtxtMenuHeaderTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            if (value is String varText)
            {
                return $"{varText}:";
                // alternativ:
                // return varText + ":";
            }
            return string.Empty;    
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Diese Funktion wäre gedacht wenn man ein TextFeld hätte wo man reinschreiben könnte
            return string.Empty;
        }
    }
}
