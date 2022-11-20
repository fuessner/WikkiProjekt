using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WikkiProjekt.DTOs;
using WikkiProjekt.Helpers;

namespace WikkiProjekt.AppWindows
{
    /// <summary>
    /// Interaktionslogik für InfoDialog.xaml
    /// </summary>
    public partial class InfoDialog : Window
    {
        private string _Message { get; set; } = "";
        private IWDialogType _DialogType { get; set; } = new();
        public string DialogInputText { get; set; } = string.Empty;
        public InfoDialog(string iMessage, IWDialogType iWDialogType)
        {
            InitializeComponent();
            // Mit Owner teilen wir der Dialogbox mit wer das Hauptfenster ist.
            // Damit wird die Dialogbox direkt im Center der App angezeigt
            // Die haben wir im xaml code mit WindowStartupLocation="CenterOwner" festgehalten
            this.Owner = GlobVar.GlobMainWindow;
          
            _Message = iMessage;
            _DialogType = iWDialogType;
            InputTextBorder.Visibility = Visibility.Collapsed;
            Grid_JA_NEIN.Visibility = Visibility.Collapsed;
            Grid_BtnOK_BtnAbbrechen.Visibility = Visibility.Collapsed;

            txtBlockMessage.Text = _Message;
            TxtBoxInput.Clear();

            switch (_DialogType)
            {
                case IWDialogType.Information:
                    LBLTitle.Content = "Information";
                    LBLTitle.Visibility = Visibility.Visible;
                    Grid_BtnOK_BtnAbbrechen.Visibility = Visibility.Visible;
                    BtnAbbrechen.Visibility = Visibility.Collapsed;   
                    break;
                case IWDialogType.Confirmation:
                    LBLTitle.Content = "Bestätigen";
                    LBLTitle.Visibility = Visibility.Visible;
                    Grid_JA_NEIN.Visibility = Visibility.Visible;
                    break;
                case IWDialogType.Warnung:
                    LBLTitle.Content = "Achtung";
                    LBLTitle.Visibility = Visibility.Visible;
                    txtBlockMessage.Foreground = Brushes.DarkRed;
                    Grid_BtnOK_BtnAbbrechen.Visibility = Visibility.Visible;
                    BtnAbbrechen.Visibility = Visibility.Collapsed;
                    break;  
                case IWDialogType.Textangabe:
                    LBLTitle.Content = "Angabe";
                    LBLTitle.Visibility = Visibility.Visible;
                    InputTextBorder.Visibility = Visibility.Visible;
                    Grid_BtnOK_BtnAbbrechen.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }

        }
        // Wenn die Dialogbox mit der Mouse bewegt wird.
        private void GridTitelBar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            // Wenn ich auf das X in der Dialogbox klicke, wird diese geschlossen.
            // Ich gebe aber noch mit, das es kein Ergebnis für die Dialogbox gibt.
            this.DialogResult = false;
            this.Close();

        }

        private void BtnNein_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void BtnJA_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void BtnAbbrechen_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            DialogInputText = TxtBoxInput.Text;
            this.Close();
        }

        private void TxtBoxInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            DialogInputText = TxtBoxInput.Text;
        }
    }
}
