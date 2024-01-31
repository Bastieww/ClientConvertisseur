using ClientConvertisseurV2.Models;
using ClientConvertisseurV2.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConvertisseurV2.ViewsModels
{
    public class ConvertisseurEuroViewModel : ObservableObject
    {
        private Devise deviseSelectionnee;
        private ObservableCollection<Devise> devises;
        public double TxEuros { get; set; }
        private double txDevises;

        public ConvertisseurEuroViewModel()
        {
            GetDataOnLoadAsync();
        }

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


        private async Task GetDataOnLoadAsync()
        {
            WSService service = new WSService("https://localhost:7088/api/");
            List<Devise> result = await service.GetDevisesAsync("devises");

            if (result == null)
            {
                throw new ArgumentException();
            }
            else
            {
                Devises = new ObservableCollection<Devise>(result);
            }
        }
    }
}
