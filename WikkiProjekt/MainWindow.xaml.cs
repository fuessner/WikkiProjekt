using ControlzEx.Theming;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WikkiDBBlib;
using WikkiDBBlib.Models;
using WikkiProjekt.Grundlage;
using WikkiProjekt.UCs;

namespace WikkiProjekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Das Layout kommt von : mahapps.com/docs/quick-start
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        // Hier werden die UC definiert, so das sie zur Laufzeit nicht immer neu geladen
        // werden müssen. Somit bleiben auch die Variablen in den UC immer enthalten
        private UCInfo? _UCInfo;
        private UCVerwaltung? _UCVerwaltung;
        private UCStatistik? _UCStatistik;

        public MainWindow()
        {
            InitializeComponent();
            // Set the window them to Dark.Red
            // Zur Laufzeit kann die Farbe geändert werden. Ist nur eine Demo was geht
            // ThemeManager.Current.ChangeTheme(this, "Light.Teal");
        }
        #region PrivateFunktionen
        private void _Init()
        {
            //_UCInfo = new UCInfo();
            //_UCVerwaltung = new UCVerwaltung();
            //_UCStatistik = new UCStatistik();
            _UCInfo = new();
            _UCVerwaltung = new();
            _UCStatistik = new();
            // Beim Starten das Menü öffnen
            UCsPlaceHolderGrid.Children.Clear();
            UCsPlaceHolderGrid.Children.Add(_UCInfo);

            // -------------------------------------------------------------------------------------
            // Einbinden des Datenbankprojektes
            // Stadt hinzufügen
            //var DBUnit = new AppDBContext();
            //var var_stadt = new Stadt() { SName="Augsburg"};
            //DBUnit.Stadt?.Add(var_stadt);
            //DBUnit.SaveChanges();  // Muss zum Abschluss gemacht werden. Sonst wird nichts geschrieben.

            //// Löschen Datensatz
            //var del_stadt = DBUnit.Stadt?.Find(1);
            //if (del_stadt != null)
            //    { 
            //    DBUnit.Stadt?.Remove(del_stadt);
            //    DBUnit.SaveChanges();
            //}
            

        }
        private void _OpenCloseFlyout(int iFlyoutIndex)
        {
            try
            {
                var flyout = this.Flyouts.Items[iFlyoutIndex] as Flyout;
                if (flyout is null) return;
                flyout.IsOpen = !flyout.IsOpen;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void _MoveMenuCursor(int iLstViewSelIndex)
        {
            var LstViewItemHeight = MainMenuListView.Height; //   ListViewItemHome.Height;
            if (iLstViewSelIndex < 0) return;
            // im XML Code steht Margin = "0 4 0 0"
            // BorderCursor ist der Name des Borders im Grid
            // BorderCursor.Margin = new Thickness(0, 4 + (LstViewItemHeight * iLstViewSelIndex),0,0);
            // Alternativ kann es auch so gemacht werden:
            ThicknessAnimation BorderCursorMarginAnmin = new ThicknessAnimation();
            BorderCursorMarginAnmin.Duration = TimeSpan.FromMilliseconds(200);
            // BorderCursorMarginAnmin.From = new Thickness(0, 0, 0, 0);
            BorderCursorMarginAnmin.To = new Thickness(0, 4 + (LstViewItemHeight * iLstViewSelIndex), 0, 0);
            BorderCursor.BeginAnimation(FrameworkElement.MarginProperty, BorderCursorMarginAnmin);

        } 
        #endregion

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _Init();
        }

        private async void BtnMinMaxResOnClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button sendBtn)
            {
                if (sendBtn is null) return;

               if(sendBtn.Name == BtnWindowMinimize.Name)
                {
                    this.WindowState = WindowState.Minimized;
                    return;
                }
                if (sendBtn.Name == BtnWindowClose.Name)
                {
                    var dialogSettings = new MetroDialogSettings()
                    {
                        AffirmativeButtonText = "Ja", 
                        NegativeButtonText = "Nein",
                        AnimateShow = true,
                        AnimateHide = true
                        //ColorScheme = MetroDialogColorScheme.Theme
                    };
                    var erg = await this.ShowMessageAsync("Achtung!", 
                        "Wollen Sie das Programm beenden?", 
                        MessageDialogStyle.AffirmativeAndNegative, dialogSettings);
                    if(erg == MessageDialogResult.Affirmative)
                    {
                        Application.Current.Shutdown();
                    }
                }
            }
            else
            {
                if (sender is ToggleButton sendTglBtn)
                {
                    if (sendTglBtn is null) return;

                    if (sendTglBtn.IsChecked == true)
                    {
                        this.WindowState = WindowState.Maximized;
                    }
                    else
                    {
                        this.WindowState = WindowState.Normal;
                    }
                }
            }
        }

        private void Grid_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            try
            {
            if (e.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            }
            catch (Exception)
            {
                //
                // throw;
            }
        }

        private void TglBtnMenueOpenClose_Click(object sender, RoutedEventArgs e)
        {
            _OpenCloseFlyout(0);
        }


        private void MainMenuListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var LstViewSelIndex = MainMenuListView.SelectedIndex;
            StkpnlSuchen.Visibility = Visibility.Collapsed;
            _MoveMenuCursor(LstViewSelIndex);
            _OpenCloseFlyout(0);
            if (TglBtnMenueOpenClose.IsChecked == true)
            {
                TglBtnMenueOpenClose.IsChecked = false;
            }

            switch (LstViewSelIndex)
            {
                case 0: 
                    // So könnte das Control in diese Form eingebunden werden
                    // Dies fällt jetzt aber weg, da wir sie am Anfang schon
                    // deklariert haben, damit die Variablen erhalten bleiben.
                    // -------------------------------------------------------
                    // Wir greifen auf die UCs zu die schon deklaiert sind mit dem _
                    // UCsPlaceHolderGrid.Children.Clear();
                    // UCsPlaceHolderGrid.Children.Add(new UCInfo());
                    UCsPlaceHolderGrid.Children.Clear();
                    UCsPlaceHolderGrid.Children.Add(_UCInfo);
                    StkpnlSuchen.Visibility = Visibility.Visible;
                    break;
                case 1:
                    UCsPlaceHolderGrid.Children.Clear();
                    UCsPlaceHolderGrid.Children.Add(_UCVerwaltung);
                    break;
                case 2:
                    UCsPlaceHolderGrid.Children.Clear();
                    UCsPlaceHolderGrid.Children.Add(_UCStatistik);
                    break;
                case 3:
                    UCsPlaceHolderGrid.Children.Clear();
                    UCsPlaceHolderGrid.Children.Add(new UCGrundlage());
                    break;
                default:
                    break;
            }


        }

        private void MetroWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            try
            {
                var flyout = this.Flyouts.Items[0] as Flyout;
                if (flyout?.IsOpen == true)
                {
                    flyout.IsOpen = false;
                    // Das icon vom Menü ändern
                    TglBtnMenueOpenClose.IsChecked = false;
                }

                
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
