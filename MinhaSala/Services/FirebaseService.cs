using System;
using System.Threading.Tasks;
using Firebase.Auth;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;

namespace MinhaSala.Services
{
    public class FirebaseService
    {
        string WebAPIKey = "AIzaSyA9Sr5gaysDBThkcGZuOwgui9UXq8OUM4Q";

        FirebaseAuthProvider authProvider;
        public FirebaseService()
        {
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIKey));
        }

        public async Task<string> SignIn(string email, string password)
        {
            try
            {
                var token = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
                if (!string.IsNullOrEmpty(token.FirebaseToken))
                {
                    App.Token = token.FirebaseToken;
                    return token.FirebaseToken;
                }
                    

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Crashes.TrackError(ex);
                if (ex.Message.Contains("internet"))
                {
                    await Shell.Current.DisplayAlert("Alerta", "Ocorreu um erro. Verifique sua conexão ou tente novamente mais tarde", "Ok");
                }
                return "";
            }
            return "";

        }
        public async Task<bool> Register(string email, string name, string password)
        {

            var token = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password, name);
            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {                
                //await authProvider.SendEmailVerificationAsync(token.FirebaseToken);                
                return true;
            }
            return false;
        }

        public async Task<bool> ResetPassword(string email)
        {
            try
            {
                await authProvider.SendPasswordResetEmailAsync(email.ToLower());
                return true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("EMAIL_NOT_FOUND"))
                    await Shell.Current.DisplayAlert("Alerta", "Email não encontrado, tente novamente", "Ok");
                else if (ex.Message.Contains("INVALID_EMAIL"))
                    await Shell.Current.DisplayAlert("Aviso", "Insira um email válido", "Ok");
                Crashes.TrackError(ex);
                return false;
            }                     
        }

        public async Task<bool> ChangeEmail(string email)
        {
            try
            {
                await authProvider.ChangeUserEmail(App.Token, email);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Crashes.TrackError(ex);

                if (ex.Message.Contains("EMAIL_EXISTS"))
                    await Shell.Current.DisplayAlert("Aviso", "Email já cadastrado", "Ok");
                else if (ex.Message.Contains("INVALID_EMAIL"))
                    await Shell.Current.DisplayAlert("Aviso", "Insira um email válido", "Ok");
                else
                    await Shell.Current.DisplayAlert("Alerta", "Ocorreu um erro, verifique o email digitado", "Ok");

                return false;
            }            
        }
    }
}

