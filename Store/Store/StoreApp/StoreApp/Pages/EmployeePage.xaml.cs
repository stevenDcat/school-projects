using System;
using System.Threading.Tasks;
using StoreApp.ViewModels;
using Xamarin.Forms;

namespace StoreApp.Pages
{
    public partial class EmployeePage : ContentPage
    {
        public EmployeePage()
        {
            InitializeComponent();
            BindingContext = new EmployeeViewModel();
        }
        

        private void Filter(object sender, EventArgs e)
        {
            var vm = BindingContext as EmployeeViewModel;
            if (vm != null)
            {
                vm.FilterSet();
            }
        }

        private void Clicked_Add_ByQuantity(object sender, EventArgs e)
        {
            var vm = BindingContext as EmployeeViewModel;
            if (vm != null)
            {
                vm.AddProduct(this, ItemType.ProductByQuantity);
                vm.Refresh();
            }
            

        }
        
        private void Clicked_Add_ByWeight(object sender, EventArgs e)
        {
            var vm = BindingContext as EmployeeViewModel;
            if (vm != null)
            {
                vm.AddProduct(this, ItemType.ProductByWeight);
            }
            

        }

        private void Clicked_Edit(object sender, EventArgs e)
        {
            
            var vm = BindingContext as EmployeeViewModel;
            if (vm != null)
            {
                vm.EditProduct(this);
            }


        }

        private void Clicked_Delete(object sender, EventArgs e)
        {
            var vm = BindingContext as EmployeeViewModel;
            if (vm != null)
            {
                vm.Remove();
            }
        }

        private void Clicked_Logout(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void Search(object sender, EventArgs e)
        {
            var vm = BindingContext as EmployeeViewModel;
            if (vm != null)
            {
                vm.Refresh();
            }
        }
        

        //Useless with server
        private void Clicked_Save(object sender, EventArgs e)
        {
            var vm = BindingContext as EmployeeViewModel;
            if (vm != null)
            {
                //vm.Save();
            }
        }
        
        //Useless with server
        private void Clicked_Load(object sender, EventArgs e)
        {
            var vm = BindingContext as EmployeeViewModel;
            if (vm != null)
            {
                //vm.Load();
            }
        }
        
    }
}
