using System;
using StoreApp.ViewModels;
using Xamarin.Forms;

namespace StoreApp.Pages
{
    public partial class PaymentDialog : ContentPage
    {
        public PaymentDialog(object o)
        {
            InitializeComponent();
            BindingContext = o as CartViewModel;
        }

        private void Clicked_Pay(object sender, EventArgs e)
        {
            var vm = BindingContext as CartViewModel;
            if( vm != null)
                vm.Pay();
            Navigation.PopModalAsync();
        }

        private void Clicked_Back(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}