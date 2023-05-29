using System;
using System.Linq;
using ProductLibrary.Models;
using ProductLibrary.Services;
using StoreApp.ViewModels;
using Xamarin.Forms;

namespace StoreApp.Pages
{
    public partial class ByQuantityDialog : ContentPage
    {
        
        public ByQuantityDialog()
        {
            InitializeComponent();
            BindingContext = new ByQuantityViewModel();
        }
        
        public ByQuantityDialog(ProductByQuantity selectedByQuantity, bool value = true)
        {
            InitializeComponent();
            if (!value)
            {
                
                if (selectedByQuantity != null)
                {
                    Row5.IsEnabled = Row5.IsVisible = true;
                    var max = ProductService.Current.inventoryList.FirstOrDefault(t => t.Id == selectedByQuantity.Id);
                    if((max as ProductByQuantity).Quantity != 0) 
                        Stepper.Maximum = (max as ProductByQuantity).Quantity;
                    else
                    {
                        Stepper.IsEnabled = false;
                    }
                }
            }

            BoGoRow.IsEnabled = value;
            BoGoRow.IsVisible = value;
            NameEntry.IsEnabled = value;
            DescriptionEntry.IsEnabled = value;
            PriceEntry.IsEnabled = value;
            QuantityEntry.IsEnabled = value;
            BindingContext = selectedByQuantity;
            
        }

        private void Ok_Clicked(object sender, EventArgs e)
        {
            if (!Row5.IsEnabled)
            {
                if (BindingContext is ProductByQuantity)
                {
                    ProductService.Current.AddOrUpdateInventory(BindingContext as ProductByQuantity);
                    Navigation.PopModalAsync();
                }
                else
                {
                    var vm = BindingContext as ByQuantityViewModel;
                    ProductService.Current.AddOrUpdateInventory(vm.Add());
                    Navigation.PopModalAsync();
                }
            }
            else
            {
                if (BindingContext is ProductByQuantity)
                {
                    var bc = BindingContext as ProductByQuantity;
                    var product = new ProductByQuantity();
                    product.Id = bc.Id;
                    product.BoGo = bc.BoGo;
                    product.Name = bc.Name;
                    product.Description = bc.Description;
                    product.Price = bc.Price;
                    product.Quantity = int.Parse(StepperEntry.Text);
                    ProductService.Current.AddOrUpdateCart(product);
                    Navigation.PopModalAsync();
                }
            }
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void BoGo_Yes(object sender, CheckedChangedEventArgs e)
        {
            if (!Row5.IsEnabled)
            {
                if (BoGoYes.IsChecked )
                {
                    if (BindingContext is ProductByQuantity)
                    {
                        (BindingContext as ProductByQuantity).BoGo = true;
                    }
                    else
                    {
                        var vm = BindingContext as ByQuantityViewModel;
                        vm.BoGo = true;
                    }
                    
                    if(BoGoNo.IsChecked) 
                        BoGoNo.IsChecked = false;

                }
                
                
            }
           
        }
        
        private void BoGo_No(object sender, CheckedChangedEventArgs e)
        {
            if (!Row5.IsEnabled)
            {
                if (BoGoNo.IsChecked)
                {
                    if (BindingContext is ProductByQuantity)
                    {
                        (BindingContext as ProductByQuantity).BoGo = false;
                    }
                    else
                    {
                        var vm = BindingContext as ByQuantityViewModel;
                        vm.BoGo = false;
                    }

                    if (BoGoYes.IsChecked)
                        BoGoYes.IsChecked = false;

                }
                
            }
           
        }
    }
}