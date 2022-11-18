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
        public string _DialogInputText { get; set; } = string.Empty;
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

            switch(_DialogType)
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
    }
}
