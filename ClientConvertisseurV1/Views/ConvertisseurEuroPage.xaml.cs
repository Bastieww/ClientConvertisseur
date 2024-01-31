using ClientConvertisseurV1.Models;
using ClientConvertisseurV1.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClientConvertisseurV1.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConvertisseurEuroPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Devise deviseSelectionnee;
        private ObservableCollection<Devise> devises;
        public double TxEuros { get; set; }
        private double txDevises;


        public ObservableCollection<Devise> Devises 
        {
            get { return devises; }
            set
            {
                devises = value;
                OnPropertyChanged();
            }
        }
        
        public double TxDevises
        {
            get { return txDevises; }
            set
            {
                txDevises = value;
                OnPropertyChanged();
            }
        }

        public Devise DeviseSelectionnee
        {
            get { return deviseSelectionnee; }
            set
            {
                deviseSelectionnee = value;
                OnPropertyChanged();
            }
        }
     
        public ConvertisseurEuroPage()
        {
            this.InitializeComponent();
            this.DataContext = this;
            GetDataOnLoadAsync();
        }

       

        private async Task GetDataOnLoadAsync()
        {
            WSService service = new WSService("https://localhost:7088/api/");
            List<Devise> result = await service.GetDevisesAsync("devises");
            
            if(result == null)
            {
                APINonDisponible();
            }
            else
            {
                Devises = new ObservableCollection<Devise>(result); 
            }
        }

        public void btConvertirClick(object sender, RoutedEventArgs e)
        {
            if(DeviseSelectionnee == null)
            {
                DeviseNonSelectionnee(); //se déclenceh avec du délai (sans doute à cause d'un time out pour essayer de trouver l'API)
            }
            else
            {
                double montantDevise = TxEuros * DeviseSelectionnee.Taux;
                TxDevises = montantDevise;
            }
           
        }

        private async void DeviseNonSelectionnee()
        {

            ContentDialog deviseNonSelectionneeDialog = new ContentDialog
            {
                Title = "Devise non saisie",
                Content = "Sélectionnez une devise",
                CloseButtonText = "Ok",
                XamlRoot = this.Content.XamlRoot //Parce que c'est lancé depuis une frame et pas une fenetre
            };
            ContentDialogResult result = await deviseNonSelectionneeDialog.ShowAsync();
        }

        private async void APINonDisponible()
        {

            ContentDialog APINonDisponibleDialog = new ContentDialog
            {
                Title = "API Non disponible !",
                Content = "Vérifiez que le service est bien lancè.",
                CloseButtonText = "Ok",
                XamlRoot = this.Content.XamlRoot //Parce que c'est lancé depuis une frame et pas une fenetre
            };
            ContentDialogResult result = await APINonDisponibleDialog.ShowAsync();
        }


        protected void OnPropertyChanged([CallerMemberName]string? name = null)
        {
            PropertyChangedEventHandler? handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
