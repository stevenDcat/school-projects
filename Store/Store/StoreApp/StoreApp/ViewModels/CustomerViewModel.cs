using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ProductLibrary.Models;
using ProductLibrary.Services;
using StoreApp.Pages;
using Xamarin.Forms;

namespace StoreApp.ViewModels
{
    public class CustomerViewModel: INotifyPropertyChanged
    {
        public static string CurrentCartName { get; set; }
        private bool Filter;
        public string Query { get; set; }
        private Product selectedProduct;
        private ProductService _productService;
        
        public CustomerViewModel()
        {
            _productService = ProductService.Current;
            NotifyPropertyChanged("Products");
        }

        public ObservableCollection<Product> Products
        {
            get
            {
                if (_productService == null)
                    return new ObservableCollection<Product>();
                if (!string.IsNullOrEmpty(Query))
                    return new ObservableCollection<Product>(
                        _productService.inventoryList.Where(i => i.Name.ToUpper().Contains(Query.ToUpper()) ||
                                                                 i.Description.ToUpper().Contains(Query.ToUpper())).OrderBy(i => i.Name));
                if(Filter)
                    return new ObservableCollection<Product>(
                        _productService.inventoryList.OrderBy( i => i.Price));
                else
                    return new ObservableCollection<Product>(_productService.inventoryList);
            }

        }

        public Product SelectedProduct
        {
            get => selectedProduct;

            set
            {
                selectedProduct = value;
                NotifyPropertyChanged();
            }
        }
        
        
        public void FilterSet()
        {
            Filter = !Filter;
            NotifyPropertyChanged("Products");
            
        }
        

        public async void Add(ContentPage page)
        {
            
            if (selectedProduct != null)
            {
                ContentPage diag = null;
                if (selectedProduct is ProductByQuantity)
                {
                    diag = new ByQuantityDialog(selectedProduct as ProductByQuantity,false);
                    await page.Navigation.PushModalAsync(diag);
                    diag.Disappearing += (object o, EventArgs e) => { NotifyPropertyChanged("Products"); };
                }
                else if (selectedProduct is ProductByWeight)
                {
                    diag = new ByWeightDialog(selectedProduct as ProductByWeight,false);
                    await page.Navigation.PushModalAsync(diag);
                    diag.Disappearing += (object o, EventArgs e) => { NotifyPropertyChanged("Products"); };
                }
            }
        }
        
        public void Refresh()
        {
            NotifyPropertyChanged("Products");
        }

        public async void ToCart(ContentPage page)
        {
            var diag = new CartPage();
            await page.Navigation.PushModalAsync(diag);
            diag.Disappearing += (object o, EventArgs e) => { NotifyPropertyChanged("Products"); };
            
        }
        
        public enum ItemType{ ProductByQuantity, ProductByWeight }
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}