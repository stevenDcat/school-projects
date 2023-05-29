using System;
using StoreApp.ViewModels;
using Xamarin.Forms;

namespace StoreApp.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new StoreLogin();
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            var vm = BindingContext as StoreLogin;
            if (vm != null)
            {
                vm.Login(this);
            }
        }
        
    }
}