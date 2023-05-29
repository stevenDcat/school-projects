using System;
using StoreApp.ViewModels;
using Xamarin.Forms;


namespace StoreApp.Pages
{
    public partial class SaveDialog : ContentPage
    {
        public SaveDialog(object o)
        {
            InitializeComponent();
            BindingContext = o as CartViewModel;
        }

        private void Clicked_Save(object sender, EventArgs e)
        {
            var vm = BindingContext as CartViewModel;
            if(vm != null)
                vm.SaveCart();
            Navigation.PopModalAsync();
        }

        private void Clicked_Cancel(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}