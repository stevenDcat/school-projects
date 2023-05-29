using System;
using System.Linq;
using ProductLibrary.Models;
using ProductLibrary.Services;
using StoreApp.ViewModels;
using Xamarin.Forms;

namespace StoreApp.Pages
{
    public partial class ByWeightDialog : ContentPage
    {
        public ByWeightDialog()
        {
            InitializeComponent();
            BindingContext = new ByWeightViewModel();
        }

        public ByWeightDialog(ProductByWeight selectedByWeight)
        {
            InitializeComponent();
            BindingContext = selectedByWeight;
            
        }
        
        
        public ByWeightDialog(ProductByWeight selectedByWeight, bool value = true)
        {
            InitializeComponent();
            if (!value)
            {
                
                if (selectedByWeight != null)
                {
                    var max = ProductService.Current.inventoryList.FirstOrDefault(t => t.Id == selectedByWeight.Id);
                    if((max as ProductByWeight).Weight != 0) 
                        Stepper.Maximum = (max as ProductByWeight).Weight;
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
            WeightEntry.IsEnabled = value;
            Row5.IsEnabled = Row5.IsVisible = true;
            BindingContext = selectedByWeight;
        }

        private void Ok_Clicked(object sender, EventArgs e)
        {
            if (!Row5.IsEnabled)
            {
                if (BindingContext is ProductByWeight)
                {
                    ProductService.Current.AddOrUpdateInventory(BindingContext as ProductByWeight);
                    Navigation.PopModalAsync();
                }
                else
                {
                    var vm = BindingContext as ByWeightViewModel;
                    ProductService.Current.AddOrUpdateInventory(vm.Add());
                    Navigation.PopModalAsync();
                }
                
            }
            else
            {
                if (BindingContext is ProductByWeight)
                {
                    var bc = BindingContext as ProductByWeight;
                    var product = new ProductByWeight();
                    product.Id = bc.Id;
                    product.BoGo = bc.BoGo;
                    product.Name = bc.Name;
                    product.Description = bc.Description;
                    product.Price = bc.Price;
                    product.Weight = double.Parse(StepperEntry.Text);
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
                if (BoGoYes.IsChecked)
                {
                    if (BindingContext is ProductByWeight)
                    {
                        (BindingContext as ProductByWeight).BoGo = true;
                    }
                    else
                    {
                        var vm = BindingContext as ByWeightViewModel;
                        vm.BoGo = true;
                    }

                    if (BoGoNo.IsChecked)
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
                    if (BindingContext is ProductByWeight)
                    {
                        (BindingContext as ProductByWeight).BoGo = false;
                    }
                    else
                    {
                        var vm = BindingContext as ByWeightViewModel;
                        vm.BoGo = false;
                    }

                    if (BoGoYes.IsChecked)
                        BoGoYes.IsChecked = false;

                }
                    
            }
        }
    }
}