using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AppCenter.Crashes;
using MinhaSala.Interfaces;
using MinhaSala.Services;
using MinhaSala.Views;
using Xamarin.Forms;

namespace MinhaSala.ViewModels
{
    [QueryProperty(nameof(Registration), "Id")]
    public class SignUpPageViewModel : BaseViewModel
    {
        FirebaseService auth;

        AirtableService service;

        #region paramShell
        string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Registration
        {
            set
            {
                Test(value);
            }
        }
        void Test(string id)
        {
            Id = id;
        }
        #endregion

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

        string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged("ConfirmPassword");
            }
        }

        public ICommand OnSignUpCommand => new Command(async () => await OnSignUpAsync());

        public SignUpPageViewModel()
        {
            auth = new FirebaseService();
            service = new AirtableService();
        }

        private async Task OnSignUpAsync()
        {
            try
            {
                if (String.IsNullOrEmpty(Username))
                {
                    await Shell.Current.DisplayAlert("Alerta", "Digite seu nome", "Ok");
                    return;
                }
                if (Username.Length < 4)
                {
                    await Shell.Current.DisplayAlert("Alerta", "Digite um nome válido", "Ok");
                    return;
                }
                if (String.IsNullOrEmpty(Email))
                {
                    await Shell.Current.DisplayAlert("Alerta", "Digite seu email", "Ok");
                    return;
                }
                if (Password.Length < 6)
                {
                    await Shell.Current.DisplayAlert("Alerta", "Sua senha deve conter no minimo 6 caracteres", "Ok");
                    return;
                }
                if (String.IsNullOrEmpty(Password))
                {
                    await Shell.Current.DisplayAlert("Alerta", "Digite uma senha", "Ok");
                    return;
                }
                if (String.IsNullOrEmpty(ConfirmPassword))
                {
                    await Shell.Current.DisplayAlert("Alerta", "Confirme sua senha", "Ok");
                    return;
                }
                if (Password != ConfirmPassword)
                {
                    await Shell.Current.DisplayAlert("Alerta", "Suas senhas não correspondem", "Ok");
                    return;
                }

                bool isSave = await auth.Register(Email, Username, Password);
                if (isSave)
                {
                    await Shell.Current.DisplayAlert("Sucesso!", "Cadastro Realizado", "Ok");
                    var result = await service.CreateUser(Id, Username, Email);
                    var resultId = result.Select(m => m.Matricula);
                    var id = string.Join("", resultId);
                    App.Username = Username;
                    App.Current.MainPage = new AppShell();
                }
                else
                    await Shell.Current.DisplayAlert("Alerta", "Ocorreu um erro no cadastro", "Ok");

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("EMAIL_EXISTS"))
                    await Shell.Current.DisplayAlert("Aviso", "Email já cadastrado", "Ok");
                else if (ex.Message.Contains("INVALID_EMAIL"))
                    await Shell.Current.DisplayAlert("Aviso", "Insira um email válido", "Ok");
                else
                    Console.WriteLine(ex.Message);

                Crashes.TrackError(ex);
            }
        }

    }
}

