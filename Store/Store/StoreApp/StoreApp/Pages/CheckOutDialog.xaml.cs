using System;
using System.Linq;
using ProductLibrary.Services;
using StoreApp.ViewModels;
using Xamarin.Forms;


namespace StoreApp.Pages
{
    public partial class CheckOutDialog : ContentPage
    {
        public CheckOutDialog(object o)
        {
            InitializeComponent();
            BindingContext = o as CartViewModel;
            if (!ProductService.Current.cartList.Any())
                PayButton.IsEnabled = false;
        }

        private void Clicked_Pay(object sender, EventArgs e)
        {
            
            var diag = new PaymentDialog(BindingContext as CartViewModel);
            Navigation.PushModalAsync(diag);
        }

        private void Clicked_Back(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();

        }
    }
}