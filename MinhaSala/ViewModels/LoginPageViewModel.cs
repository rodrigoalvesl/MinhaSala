using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Firebase.Auth;
using Microsoft.AppCenter.Crashes;
using MinhaSala.Interfaces;
using MinhaSala.Services;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MinhaSala.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        FirebaseService auth;
        AirtableService service;

        public bool LembrarUsuario
        {
            get => Preferences.Get(nameof(LembrarUsuario), false);
            set => Preferences.Set(nameof(LembrarUsuario), value);
        }

        public string SavedEmail
        {
            get => Preferences.Get(nameof(SavedEmail), string.Empty);
            set => Preferences.Set(nameof(SavedEmail), value);
        }

        public string SavedPassword
        {
            get => Preferences.Get(nameof(SavedPassword), string.Empty);
            set => Preferences.Set(nameof(SavedPassword), value);
        }

        bool _saveUser;
        public bool SaveUser
        {
            get => _saveUser;
            set
            {
                _saveUser = value;
                OnPropertyChanged("SaveUser");
                LembrarUsuario = SaveUser;
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
        string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public ICommand LoginCommand => new Command(async () => await LoginAsync());
        public ICommand ForgotCommand => new Command(async () => await ForgotAsync());
        public ICommand SignUpCommand => new Command(async () => await SignUpAsync());

        public LoginPageViewModel()
        {
            auth = new FirebaseService();
            service = new AirtableService();
            SaveUser = LembrarUsuario;

            if (!LembrarUsuario)
            {
                Email = string.Empty;
                Password = string.Empty;
            }                
            Email = SavedEmail;
            Password = SavedPassword;
        }

        private async Task SignUpAsync()
        {
            await Shell.Current.GoToAsync("/CheckPage");
        }

        public async Task LoginAsync()
        {
            try
            {
                var id = await service.CheckUser(Email.ToLower());
                App.Username = id.Nome;

                var token = await auth.SignIn(Email.ToLower(), Password);
                if (!string.IsNullOrEmpty(token))
                {
                    if (LembrarUsuario)
                    {
                        SavedEmail = Email;
                        SavedPassword = Password;
                    }
                    else
                    {
                        SavedEmail = string.Empty;
                        SavedPassword = string.Empty;
                    }
                    await Shell.Current.GoToAsync($"//Tabs/Home?Id={id.Matricula}");             
                }
                else
                    await Shell.Current.DisplayAlert("Alerta", "Usuário e/ou Senha Inválidos.", "Ok");
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                await Shell.Current.DisplayAlert("Alerta", "Ocorreu um erro, tente novamente mais tarde.", "Ok");
                Console.WriteLine(ex.Message);
            }
        }

        private async Task ForgotAsync()
        {
            await Shell.Current.GoToAsync("/ForgotPage");
        }

        private void CheckWhetherTheUserIsSignIn()
        {
            // if (!auth.IsSignIn())
            //Shell.Current.GoToAsync("//Tabs/Home");
        }
    }
}