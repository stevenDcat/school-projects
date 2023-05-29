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
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private bool Filter;

        public string Query { get; set; }
        private Product selectedProduct;
        private ProductService _productService;
        
        public EmployeeViewModel()
        {
            _productService = ProductService.Current;
        }

        public ObservableCollection<Product> Products
        {
            get
            {
                if (_productService == null)
                    return new ObservableCollection<Product>();
                if(!string.IsNullOrEmpty(Query))
                    return new ObservableCollection<Product>(
                        _productService.inventoryList.Where(i => i.Name.ToUpper().Contains(Query.ToUpper()) || i.Description.ToUpper().Contains(Query.ToUpper())).OrderBy( i => i.Name));
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

        public void Remove()
        {
            if (selectedProduct != null)
            {
                ProductService.Current.DeleteFromInventory(selectedProduct);
                NotifyPropertyChanged("Products");

            }
            
        }

        public async void AddProduct(ContentPage page, ItemType iType)
        {
            ContentPage diag = null;
            if (iType == ItemType.ProductByQuantity)
                diag = new ByQuantityDialog();
            else if (iType == ItemType.ProductByWeight)
                diag = new ByWeightDialog();
            else
                throw new NotImplementedException();
            await page.Navigation.PushModalAsync(diag);
            diag.Disappearing += (object o, EventArgs e) => { NotifyPropertyChanged("Products"); };
        }

        public async void EditProduct(ContentPage page)
        {
            if (selectedProduct != null)
            {
                ContentPage diag = null;
                if (selectedProduct is ProductByQuantity)
                {
                    diag = new ByQuantityDialog(selectedProduct as ProductByQuantity);
                    await page.Navigation.PushModalAsync(diag);
                    diag.Disappearing += (object o, EventArgs e) => { NotifyPropertyChanged("Products"); };

                }
                else if (selectedProduct is ProductByWeight)
                {
                    diag = new ByWeightDialog(selectedProduct as ProductByWeight);
                    await page.Navigation.PushModalAsync(diag);
                    diag.Disappearing += (object o, EventArgs e) => { NotifyPropertyChanged("Products"); };
                }
                
            }
        }

        public void Refresh()
        {
            NotifyPropertyChanged("Products");
        }

        //Useless with server
        public void Save()
        {
           // _productService.InventorySave();
        }
        
        //Useless with server
        public void Load()
        {
            //_productService.InventoryLoad();
            NotifyPropertyChanged("Products");
        }
        
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

public enum ItemType{ ProductByQuantity, ProductByWeight }