using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MinhaSala.Interfaces;
using MinhaSala.Services;
using MinhaSala.Views;
using Xamarin.Forms;

namespace MinhaSala.ViewModels
{
    public class LogoutPageViewModel : BaseViewModel
    {
        FirebaseService auth;

        string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }

        public ICommand OnSignOutCommand => new Command(async () => await OnSignOutAsync());
        public ICommand ChangeCommand => new Command(async () => await ChangeUserDataAsync());

        public LogoutPageViewModel()
        {
            auth = new FirebaseService();
            Username = App.Username;
        }

        private async Task OnSignOutAsync()
        {
            App.Current.MainPage = new AppShell();
        }

        private async Task ChangeUserDataAsync()
        {
            await Shell.Current.GoToAsync("ChangePage");
        }

    }
}

