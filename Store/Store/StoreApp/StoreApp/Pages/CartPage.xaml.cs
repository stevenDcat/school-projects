using System;
using StoreApp.ViewModels;
using Xamarin.Forms;

namespace StoreApp.Pages
{
    public partial class CartPage : ContentPage
    {
        public CartPage()
        {
            InitializeComponent();
            BindingContext = new CartViewModel();
        }
        
        private void Search(object sender, EventArgs e)
        {
            var vm = BindingContext as CartViewModel;
            if(vm != null) 
                vm.RefreshCart();
        }

        private void Clicked_Edit(object sender, EventArgs e)
        {
            var vm = BindingContext as CartViewModel;
            if(vm != null) 
                vm.EditProduct(this);

        }

        private void Clicked_Delete(object sender, EventArgs e)
        {
            var vm = BindingContext as CartViewModel;
            if(vm != null) 
                vm.Remove();
            
        }

        private void Clicked_CheckOut(object sender, EventArgs e)
        {
            var diag = new CheckOutDialog(BindingContext as CartViewModel);
            Navigation.PushModalAsync(diag);
        }
        
        private void Clicked_Save(object sender, EventArgs e)
        {
            var vm = BindingContext as CartViewModel;
            if(vm != null) 
                vm.SaveOrLoadCart(this,vm);

        }
        

        private void Clicked_Back(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void Filter(object sender, EventArgs e)
        {
            var vm = BindingContext as CartViewModel;
            if(vm != null )
                vm.FilterSet();
           
        }
    }
}