using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AppCenter.Crashes;
using MinhaSala.Services;
using Xamarin.Forms;

namespace MinhaSala.ViewModels
{
    public class ForgotPasswordPageViewModel : BaseViewModel
    {

        FirebaseService auth;

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

        public ICommand ResetCommand => new Command(async () => await ForgotAsync());

        public ForgotPasswordPageViewModel()
        {
            auth = new FirebaseService();
            
        }

        private async Task ForgotAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(Email))
                {
                    await Shell.Current.DisplayAlert("Aviso", "Digite seu email", "Ok");
                    return;
                }
                var isSend = await auth.ResetPassword(Email.ToLower());
                if (isSend)
                {
                    await Shell.Current.DisplayAlert("Sucesso", "As instruções para redefinição de senha foram enviadas ao seu email", "Ok");
                    await Shell.Current.GoToAsync("///Login");
                }
                else                
                    await Shell.Current.DisplayAlert("Alerta", "Ocorreu um erro. Verifique o email digitado ou tente novamente mais tarde", "Ok");                


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

