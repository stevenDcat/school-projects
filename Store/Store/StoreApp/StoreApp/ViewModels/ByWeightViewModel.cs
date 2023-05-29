using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProductLibrary.Models;

namespace StoreApp.ViewModels
{
    public class ByWeightViewModel: INotifyPropertyChanged
    {
        
        public int Id { get; set; }
        public bool BoGo;
        private string name = string.Empty;
        private string description = string.Empty;
        private double weight;
        private double price;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                NotifyPropertyChanged();
            }
        } 
        
        public string Description
        {
            get => description;
            set
            { 
                description = value;
                NotifyPropertyChanged();
            }
        }

        public double Weight
        {
            get => weight;
            set
            {
                weight = value;
                NotifyPropertyChanged();
            }
        }
        
        public double Price
        {
            get => price;
            set
            {
                price = value;
                NotifyPropertyChanged();
            }
        }

        public ProductByWeight Add()
        {
            var product = new ProductByWeight();
            product.Id = Id;
            if (BoGo)
                product.BoGo = BoGo;
            product.Name = name;
            product.Description = description;
            product.Weight = weight;
            product.Price = price;
            return product;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}