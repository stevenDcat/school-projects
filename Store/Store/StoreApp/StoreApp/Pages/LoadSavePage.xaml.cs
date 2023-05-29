using System;
using ProductLibrary.Services;
using StoreApp.ViewModels;
using Xamarin.Forms;


namespace StoreApp.Pages
{
    public partial class LoadSavePage : ContentPage
    {
        public LoadSavePage(object o)
        {
            InitializeComponent();
            BindingContext = o as CartViewModel;
        }

        private void Search(object sender, EventArgs e)
        {
            var vm = BindingContext as CartViewModel;
            if(vm != null)
                vm.RefreshSaves();
            
        }

        private void Clicked_Save(object sender, EventArgs e)
        {
            var vm = BindingContext as CartViewModel;
            if(vm != null)
                vm.SetSave(this, BindingContext as CartViewModel);
                
        }

        private void Clicked_Load(object sender, EventArgs e)
        {
            var vm = BindingContext as CartViewModel;
            if(vm != null)
                vm.LoadCart();

        }

        private void Clicked_Back(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void Clicked_Delete(object sender, EventArgs e)
        {
            var vm = BindingContext as CartViewModel;
            if (vm != null)
                vm.DeleteSaves();
        }
    }
}