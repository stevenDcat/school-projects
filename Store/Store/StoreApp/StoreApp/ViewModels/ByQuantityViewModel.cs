
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProductLibrary.Models;

namespace StoreApp.ViewModels
{
    public class ByQuantityViewModel: INotifyPropertyChanged
    {
        public int Id;
        public bool BoGo;
        private string name = string.Empty;
        private string description = string.Empty;
        private int quantity;
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

        public int Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
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

        public ProductByQuantity Add()
        {
            var product = new ProductByQuantity();
            if (BoGo) 
                product.BoGo = BoGo;
            product.Id = Id;
            product.Name = name;
            product.Description = description;
            product.Quantity = quantity;
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