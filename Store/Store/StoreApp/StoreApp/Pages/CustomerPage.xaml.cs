using System;
using StoreApp.ViewModels;
using Xamarin.Forms;

namespace StoreApp.Pages
{
    public partial class CustomerPage : ContentPage
    {
        public CustomerPage()
        {
            InitializeComponent();
            BindingContext = new CustomerViewModel();
        }

        private void Clicked_Add(object sender, EventArgs e)
        {
            var vm = BindingContext as CustomerViewModel;
            if(vm != null) 
                vm.Add(this);
        }

        private void Clicked_Logout(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void Filter(object sender, EventArgs e)
        {
            var vm = BindingContext as CustomerViewModel;
            if(vm != null) 
                vm.FilterSet();
        }

        private void Search(object sender, EventArgs e)
        {
            var vm = BindingContext as CustomerViewModel;
            if(vm != null) 
                vm.Refresh();
        }


        private void Clicked_GoToCart(object sender, EventArgs e)
        {
            var vm = BindingContext as CustomerViewModel;
            if (vm != null) 
                vm.ToCart(this);
        }

       
    }
}