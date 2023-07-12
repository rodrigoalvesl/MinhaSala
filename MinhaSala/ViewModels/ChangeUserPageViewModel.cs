using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MinhaSala.Services;
using Xamarin.Forms;

namespace MinhaSala.ViewModels
{
    public class ChangeUserPageViewModel : BaseViewModel
    {
        FirebaseService service;

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
        string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        public ICommand ChangeCommand => new Command(async () => await ChangeUserAsync());

        public ChangeUserPageViewModel()
        {
            service = new FirebaseService();
        }

        private async Task ChangeUserAsync()
        {
            var changed = await service.ChangeEmail(Email);
            if (changed)
            {
                await Shell.Current.DisplayAlert("Sucesso", "Endereço de email atualizado", "Ok");
                await Shell.Current.GoToAsync("///Login");
            }           
        }
    }
}

