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
    public class CartViewModel: INotifyPropertyChanged
    {
        private ProductService _productService;
        

        ////////////////////////////////////////////////////////////////// CART CONTROLLERS
       
       private bool Filter;
       public void FilterSet()
       {
           Filter = !Filter;
           NotifyPropertyChanged("Products");
       }
       public string CartQuery { get; set; }
       
       private Product selectedProduct;
       public Product SelectedProduct
       {
           get => selectedProduct;
           set
           {
               selectedProduct = value;
               NotifyPropertyChanged();
           }
       }

       
       public CartViewModel()
       {
           _productService = ProductService.Current;
           NotifyPropertyChanged("SaveFiles");
           NotifyPropertyChanged("Products");

       }
       
       public ObservableCollection<Product> Products
       {
           get
           {
               if (_productService == null)
                   return new ObservableCollection<Product>();
               if (!string.IsNullOrEmpty(CartQuery))
                   return new ObservableCollection<Product>(
                       _productService.cartList.Where(i => i.Name.ToUpper().Contains(CartQuery.ToUpper()) ||
                                                           i.Description.ToUpper().Contains(CartQuery.ToUpper())).OrderBy(i => i.Name));
               if (Filter)
                   return new ObservableCollection<Product>(
                       _productService.cartList.OrderBy( i => i.TotalPrice));
               else
                   return new ObservableCollection<Product>(_productService.cartList);
           }

       }
       
       public async void EditProduct(ContentPage page)
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
       
       public void Remove()
       {
           if (selectedProduct != null)
           {
               ProductService.Current.DeleteFromCart(selectedProduct);
               NotifyPropertyChanged("Products");
               selectedProduct = null;
           }
       }
       
       public void RefreshCart()
       {
           NotifyPropertyChanged("Products");
       }
       public async void SaveOrLoadCart(ContentPage page,object o1)
       {
           var diag = new LoadSavePage(o1);
           await page.Navigation.PushModalAsync(diag);
           diag.Disappearing += (object o2, EventArgs e) => { NotifyPropertyChanged("Products"); };
       }
       
       
       ////////////////////////////////////////////////////////////////// SAVE CONTROLLERS
       
       public string SaveQuery { get; set; }
       public string Filename { get; set; }
       
       private string selectedFile;
       
       public string SelectedFile
       {
           get => selectedFile;
           set
           {
               selectedFile = value;
               NotifyPropertyChanged();
           }
       }
       
       
       public ObservableCollection<string> SaveFiles
       {
           get
           {
               if (ProductService.Current == null)
                   return new ObservableCollection<string>();
               if (!string.IsNullOrEmpty(SaveQuery))
                   return new ObservableCollection<string>(
                       _productService.CartSaves.Where(i => i.ToUpper().Contains(SaveQuery.ToUpper())));
               else
                   return new ObservableCollection<string>(_productService.CartSaves);
           }
       }
       
       public void SaveCart()
       {
           CustomerViewModel.CurrentCartName = Filename;
           if (!string.IsNullOrEmpty(CustomerViewModel.CurrentCartName))
           {
               _productService.CartSave(CustomerViewModel.CurrentCartName);
           }
           
       }
       
       public void LoadCart()
       {
           if (selectedFile != null)
           {
               CustomerViewModel.CurrentCartName= selectedFile;
               _productService.CartLoad(CustomerViewModel.CurrentCartName);
               selectedFile = null;
               NotifyPropertyChanged("SelectedFile");
           }
       }
       
       public void RefreshSaves()
       { 
           NotifyPropertyChanged("SaveFiles");
       }
       
       public void DeleteSaves()
       {
           if (selectedFile != null)
           {
               _productService.RemoveCartSave(selectedFile);
               selectedFile = null;
               NotifyPropertyChanged("SaveFiles");
               NotifyPropertyChanged("SelectedFile");

           }
       }

       public async void SetSave(ContentPage page, object o1)
       {
           var diag = new SaveDialog(o1); 
           await page.Navigation.PushModalAsync(diag);
           diag.Disappearing += (object o2, EventArgs e) => { NotifyPropertyChanged("SaveFiles"); };
       }
       
       ////////////////////////////////////////////////////////////////// CHECKOUT CONTROLLERS
       
       public string CheckOut
       {
           get => _productService.CartCheckout();
       }
       
       public void Pay()
       {
           _productService.Pay();
           _productService.RemoveCartSave(CustomerViewModel.CurrentCartName);
           CustomerViewModel.CurrentCartName = string.Empty;
           NotifyPropertyChanged("SaveFiles");
           NotifyPropertyChanged("Products");
           NotifyPropertyChanged("CheckOut");
           
       }
       
       
       //////////////////////////////////////////////////////////////////
       public enum ItemType{ ProductByQuantity, ProductByWeight }
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}