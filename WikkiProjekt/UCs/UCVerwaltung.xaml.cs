using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
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
using WikkiDBBlib.Models.ViewModels;
using WikkiProjekt.AppWindows;
using WikkiProjekt.Helpers;
using WikkiProjekt.Validators;
using ValidationResult = FluentValidation.Results.ValidationResult;


namespace WikkiProjekt.UCs
{
    /// <summary>
    /// Interaktionslogik für Verwaltung.xaml
    /// </summary>
    public partial class UCVerwaltung : UserControl
    {
        // Globale Variablen
        private string? _SelectedFilePath = null; //  string.Empty;
        private PersonStadtVM? _SelectedPerson = null; //  string.Empty;
        private List<Stadt>? _AllCities = null;

        public UCVerwaltung()
        {
            InitializeComponent();
            _init();

        }
        private void _init()
        {
            _ShowTabPage(BtnTabAdd);

            // Radio Button vorbelegen
            RdBtnNichtInfiziert.IsChecked = true;
            RdBtnNichtAbgeschlossen.IsChecked = true;
        }
        private async void _GetAllAndShowCitiesData()
        {

            using (new WaitProgressRing(progressRing))
            {
                // ---------------------------------------------------------         
                // var cities = await Task.Run(() => DBUnit.Stadt.GetAll());
                _AllCities = await Task.Run(() => DBUnit.Stadt.GetAll()?.ToList());
                // Alternativ: var cities2 = DBUnit.Stadt.GetAll();
                // ListBoxCities
                ListBoxCities.ItemsSource = _AllCities;
                CmbBxAddCity.ItemsSource = _AllCities;
                CmbBxAddCityEdit.ItemsSource = _AllCities;
                // ---------------------------------------------------------
            }
        }

        private async void _GetAllAndShowPersonsData()
        {
            using (new WaitProgressRing(progressRing))
            {
                // ---------------------------------------------------------
                // var personen = await Task.Run(() => DBUnit.Person.GetAll(includeModel: "Stadt"));
                // Alternativ:
                // Version vor Kurs 62
                //  var personen = await Task.Run(() => DBUnit.Person.GetAll(includeModel: nameof(Stadt)));
                var Lstpersonen = await Task.Run(() => DBUnit.Person.GetAll(includeModel: nameof(Stadt)));

                if (Lstpersonen != null)
                {
                    var lstPersonStadtVM = AppHelper.GetListPersonStadtVM_from_ListPersonStadt(Lstpersonen);

                    // Alternativ: var cities2 = DBUnit.Stadt.GetAll();
                    // ListBoxCities
                    DataGridPerson.ItemsSource = lstPersonStadtVM;
                    // ---------------------------------------------------------
                }

            }
            // Nach der USING Methode wird die DISPOSE aufgerufen damit die Wartegrafik wieder ausgeblendet wird.
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
        private void _AddImageToImageControl(Image iImage)
        {
            try
            {
                var FileDialog = new OpenFileDialog()
                {
                    Filter = "Image Files|*.jpg;*.jpeg;*png",
                    Multiselect = false,
                    Title = "Bitte wählen Sie ein Bild"

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
                        _AddImageToImageControl(ImgAdd);
                    }
                    else
                    {
                        _AddImageToImageControl(ImgAddEdit);
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

        private void _ShowAllValInfos(ValidationResult iValidationResult)
        {
            foreach (var error in iValidationResult.Errors)
            {
                if (error.PropertyName == nameof(Person.PName)) { TxtBxAddNameValInfo.Text = error.ToString(); }
                if (error.PropertyName == nameof(Person.PVorname)) { TxtBxAddVornameValInfo.Text = error.ToString(); }
                if (error.PropertyName == nameof(Person.PBild)) { TxtBlckImgValInfo.Text = error.ToString(); }
                if (error.PropertyName == nameof(Person.SID)) { CmbBxAddCityValInfo.Text = error.ToString(); }
            }
        }
        private void _ClearAllValInfos()
        {
            // Das hier sind die Hinweisfelder unter den Eingabefelder (rot)
            // Für den Reiter ADD
            TxtBlckImgValInfo.Text = String.Empty;
            TxtBxAddNameValInfo.Text = String.Empty;
            TxtBxAddVornameValInfo.Text = String.Empty;
            CmbBxAddCityValInfo.Text = String.Empty;
            // Für den Reiter Edit
            TxtBlckImgValInfoEdit.Text = String.Empty;
            TxtBxAddNameValInfoEdit.Text = String.Empty;
            TxtBxAddVornameValInfoEdit.Text = String.Empty;
            CmbBxAddCityValInfoEdit.Text = String.Empty;

        }

        private void _ClearAllKontrols()
        {
            // Das hier sind die Hinweisfelder unter den Eingabefelder (rot)
            // Für den Reiter ADD

            ImgAdd.Source = null;
            TxtBxAddName.Clear();
            TxtBxAddVorname.Clear();
            CmbBxAddCity.SelectedIndex = -1;
            RdBtnAbgeschlossen.IsChecked = true;
            RdBtnNichtInfiziert.IsChecked = true;

            ImgAddEdit.Source = null;
            TxtBxAddNameEdit.Clear();
            TxtBxAddVornameEdit.Clear();
            CmbBxAddCityEdit.SelectedIndex = -1;
            RdBtnAbgeschlossenEdit.IsChecked = true;
            RdBtnNichtInfiziertEdit.IsChecked = true;

            _SelectedFilePath = null;

            _SelectedPerson = null;

        }
        private Person _GetPersonToAdd()
        {
            return new Person()
            {
                PBild = (_SelectedFilePath is not null) ? File.ReadAllBytes(_SelectedFilePath) : null,
                PName = TxtBxAddName.Text.Trim(),
                PVorname = TxtBxAddVorname.Text.Trim(),
                PInfiziert = (RdBtnInfiziert.IsChecked == true),
                PTestAbgeschlosen = (RdBtnAbgeschlossen.IsChecked == true),
                SID = (CmbBxAddCity.SelectedIndex < 0) ? -1 : (int)CmbBxAddCity.SelectedValue
            };

        }
        private async void BtnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            //  Validation Info Leeren
            _ClearAllValInfos();
            // Daten von Controls holen
            var personToAdd = _GetPersonToAdd();
            // Daten valedieren
            var personValidator = new AddNewPersonValidator();
            // Erste Version mit VAR da ich den Typ nicht genau kenne
            // var valResult = personValidator.Validate(personToAdd);
            // Zweite Version mit USING ob eingefügt
            // jetzt kann ich valResult genau den Type zuweisen den ich später brauche
            ValidationResult valResult = personValidator.Validate(personToAdd);
            if (valResult.IsValid == true)
            {
                using (new WaitProgressRing(progressRing))
                {
                    // Daten speichern
                    var IsOK = await Task.Run(() => DBUnit.Person.Add(personToAdd));
                    if (IsOK == true)
                    {
                        _GetAllAndShowPersonsData();
                        _ClearAllKontrols();
                        GlobVar.GlobMainWindow?.OpenBottomFlyout($"{personToAdd.PName} wurde hinzugefügt");
                    }
                }
            }
            else
            {
                _ShowAllValInfos(valResult);

            }

        }

        private async void TxtBxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = TxtBxSearch.Text.Trim();
            IEnumerable<Person>? personLst;

            using (new WaitProgressRing(progressRing))
            {
                if (text != String.Empty)
                {
                    personLst = await Task.Run(() => DBUnit.Person.GetAll(filter: p =>
                                p.PName.Contains(text) || p.PVorname.Contains(text),
                                includeModel: nameof(Stadt)));

                    // Könnte aber die Liste auch hier noch mal sortieren
                    // -> personLst?.OrderByDescending(p => p.PVorname).ToList();
                    // Nimm nur den ersten Satz
                    // -> personLst?.OrderByDescending(p => p.PVorname).Take(1).ToList();
                }
                else
                {
                    personLst = await Task.Run(() => DBUnit.Person.GetAll(includeModel: nameof(Stadt)));

                }

                if (personLst != null)
                {
                    var lstPersonStadtVM = AppHelper.GetListPersonStadtVM_from_ListPersonStadt(personLst);
                    DataGridPerson.ItemsSource = lstPersonStadtVM;
                }

            }
        }

        private void DataGridPerson_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItemEdit.IsSelected = false;
            TabItemEdit.IsSelected = true;  // Tab Control wechseln funktioniert aber nicht daher der Aufruf der Funktion
            _ShowTabBtnCursor(BtnTabEdit);
            _ClearAllValInfos();

            if (DataGridPerson.SelectedItem is not null)
            {
                var SelectedPerson = DataGridPerson.SelectedItem as PersonStadtVM;
                // Variable zwischenspeichern für Änderung die am Datensatz gemacht wurden
                // Dazu verwenden wir einen AutoMapper. Diese muss über Abhängigkeiten NuGet Pakete hinzugefügt werden
                if(SelectedPerson is not null)
                {
                    _SelectedPerson = MapperHelper.Map_PersVM_to_PersVM(SelectedPerson);
                }
               

                if(_SelectedPerson != null)
                {
                    // GridEditPerson ist der Name vom Grid im UCVerwaltung.xaml
                    // Nachdem das Hauptgrid die Daten erhalten hat, können jetzt die einzelnen Elemente wie
                    // z.B. das Image darauf zugreifen mit Source="{Binding PBitmagImage}"
                    GridEditPerson.DataContext = _SelectedPerson; 

                    // CmbBxAddCityEdit.SelectedItem = _AllCities?.Where(s => s.SID == _SelectedPerson.SID);
                    CmbBxAddCityEdit.SelectedItem = _AllCities?.FirstOrDefault(s => s.SID == _SelectedPerson.SID);
                }
            }

        }

        private void CmbBxAddCityEdit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selStadt = CmbBxAddCityEdit.SelectedItem as Stadt;
            if (selStadt != null)
            {
                if (_SelectedPerson != null)
                {
                    _SelectedPerson.SID = selStadt.SID;
                    _SelectedPerson.SName = selStadt.SName;
                }
            }

        }

        private void BtnDeletePerson_Click(object sender, RoutedEventArgs e)
        {
            if (new InfoDialog("Wollen Sie Löschen?", DTOs.IWDialogType.Confirmation).ShowDialog() == true)
            {

            }
            // Wenn die Dialogbox mit Textfeld angezeigt werden soll:
            //var infoDialog = new InfoDialog("Wollen Sie Löschen?", DTOs.IWDialogType.Textangabe);
            //if (infoDialog.ShowDialog() == true)
            //{
            //    var text =infoDialog.DialogInputText;
            //}
        }
    }
}
