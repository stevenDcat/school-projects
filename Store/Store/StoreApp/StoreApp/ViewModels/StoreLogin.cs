using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using StoreApp.Pages;
using Xamarin.Forms;


namespace StoreApp.ViewModels
{
    public class StoreLogin : INotifyPropertyChanged
    {
        private string password;
        private string username;

        public string Username
        {
            get => username;
            set
            {
                username = value;
                NotifyPropertyChanged();
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                NotifyPropertyChanged();
            }
        }

        public void Login(ContentPage page)
        {
            if (username != null)
            {
                ContentPage diag = null;
                if(username.ToUpper().Equals("EMPLOYEE"))
                {
                    diag = new EmployeePage();
                    page.Navigation.PushModalAsync(diag);
                }
                if(username.ToUpper().Equals("CUSTOMER"))
                {
                    diag= new CustomerPage();
                    page.Navigation.PushModalAsync(diag);
                }

                username = null;
                password = null;

                if(diag != null) 
                    diag.Disappearing += (sender, args) => { NotifyPropertyChanged("Username");};

            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}