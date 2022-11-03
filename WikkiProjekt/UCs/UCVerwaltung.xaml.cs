using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WikkiDBBlib.DBAccess;
using WikkiDBBlib.Models;

namespace WikkiProjekt.UCs
{
    /// <summary>
    /// Interaktionslogik für Verwaltung.xaml
    /// </summary>
    public partial class UCVerwaltung : UserControl
    {
        // Globale Variablen
        string _SelectedFilePath = string.Empty;

        public UCVerwaltung()
        {
            InitializeComponent();

            _init();
        }

        private async void _GetAllAndShowCitiesData() 
        {
            // ---------------------------------------------------------
            var cities = await Task.Run(() => DBUnit.Stadt.GetAll());
            // Alternativ: var cities2 = DBUnit.Stadt.GetAll();
            // ListBoxCities
            ListBoxCities.ItemsSource = cities;
            CmbBxAddCity.ItemsSource = cities;
            CmbBxAddCityEdit.ItemsSource = cities;
            // ---------------------------------------------------------
        }
        private async void _GetAllAndShowPersonsData() 
        {
            // ---------------------------------------------------------
            // var personen = await Task.Run(() => DBUnit.Person.GetAll(includeModel: "Stadt"));
            // Alternativ:
            var personen = await Task.Run(() => DBUnit.Person.GetAll(includeModel: nameof(Stadt)));
            // Alternativ: var cities2 = DBUnit.Stadt.GetAll();
            // ListBoxCities
            DataGridPerson.ItemsSource = personen;
            // ---------------------------------------------------------
        }

        // Wenn die UCVerwaltung das erste mal aufgerufen wird, wollen wir das
        // diese Einstellungen übernommen werden.
        private void _init()
        {
            _ShowTabPage(BtnTabAdd);

            // Radio Button vorbelegen
            RdBtnNichtInfiziert.IsChecked = true;
            RdBtnNichtAbgeschlossen.IsChecked = true;

        }
        private void _ShowTabPage(Button iSender)
        {
            _ShowTabBtnCursor(iSender);

            if (iSender == BtnTabAdd)
            {
                TabItemAdd.IsSelected = true;   
            }
            else if (iSender == BtnTabEdit)
            {
                TabItemEdit.IsSelected = true;
            } 
            else if (iSender == BtnTabCity)
            {
                TabItemCity.IsSelected = true;
            }
        }

        // Diese Funktion könnte auch in der Sub _ShowTabPage eingebunden sein
        // hier braucht es keine eigene Funktion
        private void _ShowTabBtnCursor(Button iSender)
        {
            // (Brush) ist vom Typ Farbe und wir wollen diese aus der Resourcen Datei
            // Dazu muss die Farbe aus der Resourcendatei umgewandelt werden in den Typ Brush
            RctglBtnTabAdd.Fill = Brushes.Transparent;
            RctglBtnTabEdit.Fill = Brushes.Transparent;
            RctglBtnTabCity.Fill = Brushes.Transparent;

            if (iSender == BtnTabAdd)
            {
                RctglBtnTabAdd.Fill = (Brush)Application.Current.Resources["AppBrushColorCyan"];              
            }
            else if (iSender == BtnTabEdit)
            {
                RctglBtnTabEdit.Fill = (Brush)Application.Current.Resources["AppBrushColorCyan"];
            }
            else if (iSender == BtnTabCity)
            {
                RctglBtnTabCity.Fill = (Brush)Application.Current.Resources["AppBrushColorCyan"];
            }
        }
        private void BtnTabAddOnClick(object sender, RoutedEventArgs e)
        {
            //Wenn Sender ein Button ist, so soll er den Inhalt in MyBtn speichern
            if (sender is Button MyBtn)
            {
                if (MyBtn is null) return;

                _ShowTabPage(MyBtn);
            }
        }

        private void CnvsImgDragDrop_Drop(object sender, DragEventArgs e)
        {
            try
            {
                // Filedrop zeigt nur den Daten der Datei an
                // Bei DataFormats könnte man auch eine Einschränkung machen auf z.B. TIF
                // Ich nehme nur das erste Element das per DragDrop eingefügt wurde
                // var files = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
                // Alternativ alle nehmen und dann auf das erste zugreifen
                var DropFiles = ((string[])e.Data.GetData(DataFormats.FileDrop));
                var myFotoFile = string.Empty;

                if (DropFiles is null) return;
                if (DropFiles.Length < 0) return;

                // Es ist genau ein Foto ausgewählt:
                if (DropFiles.Length == 1) myFotoFile = DropFiles[0];
                // Hier wird auf das letzt Element zugegriffen
                if (DropFiles.Length > 1) myFotoFile = DropFiles[DropFiles.Length - 1];
                // Wir wollen aber nur ein Foto darum diese MessageBox
                if (DropFiles.Length > 1)
                {
                    MessageBox.Show("Bitte wählen Sie nur ein Foto", "Achtung", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return;
                }
                if (myFotoFile == string.Empty) return;

                var FileInfo = new FileInfo(myFotoFile);
                if (FileInfo.Exists)
                {
                    // statt war könnte man hier auch bool schreiben
                    var isFileTypeOK = (FileInfo.Extension == ".png") || (FileInfo.Extension == ".jpg") || (FileInfo.Extension == ".jpeg");
                    if (isFileTypeOK)
                    {
                        _SelectedFilePath = FileInfo.FullName;
                        var img = new BitmapImage(new Uri(FileInfo.FullName));
                        ImgAdd.Source = img;
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Source);
                throw;
            }
  

        }
        private void AddImageToImageControl(Image iImage)
        {
            try
            {
                var FileDialog = new OpenFileDialog()
                {
                    Filter = "Image Files|*.jpg;*.jpeg;*png",
                    Multiselect = false,
                    Title ="Bitte wählen Sie ein Bild"
                   
                };
                var erg = FileDialog.ShowDialog();  
                if (erg == true && erg.HasValue)
                {
                    _SelectedFilePath = FileDialog.FileName;
                    var img = new BitmapImage(new Uri(_SelectedFilePath));
                    iImage.Source = img;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Source);
                throw;
            }
        }
        private void BtnAddImg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var myBtn = sender as Button;
                if (myBtn != null)
                {
                    if (myBtn == BtnAddImg)
                    {
                        AddImageToImageControl(ImgAdd);
                    }
                    else
                    {
                        AddImageToImageControl(ImgAddEdit);
                    }
                }
                   
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void BtnDatenAufruf_Click(object sender, RoutedEventArgs e)
        {
            _GetAllAndShowCitiesData();
            _GetAllAndShowPersonsData();
        }
    }
}
