using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikkiProjekt.Helpers
{
    //  IDisposable wurde per Hand hinzugefügt. Danach war der Code Unterstrichen
    //  Dann mit der Maus drauf klicken und STRG + PUNKT drücken. Dann kann die
    //  Schnittstelle public Void Dispose hinzugefügt werden
    //  WICHTIG zum Verständnis. Die Dispose funktion wird automatisch aufgerufen
    //  wenn die die Klasse nicht mehr brauche.
    //  In UCVerwaltung wird die Klasse mit using (new WaitProgressRing(progressRing)) aufgerufen
    //  (progressRing) wurde im XAML Code hinterlegt 
    public class WaitProgressRing : IDisposable

    {
        private ProgressRing _ProgressRing;

        public WaitProgressRing(ProgressRing iprogressRing)
        {
            _ProgressRing = iprogressRing;

            _ProgressRing.Visibility = System.Windows.Visibility.Visible;
        }

        public void Dispose()
        {
            _ProgressRing.Visibility = System.Windows.Visibility.Hidden;
            // throw new NotImplementedException();
        }
    }
}
