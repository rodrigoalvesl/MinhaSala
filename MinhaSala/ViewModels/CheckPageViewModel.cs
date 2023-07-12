using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MinhaSala.Services;
using MinhaSala.Views;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace MinhaSala.ViewModels
{
    public class CheckPageViewModel : BaseViewModel
    {
        public AirtableService service;

        string _registration;
        public string Registration
        {
            get => _registration;
            set
            {
                _registration = value;
                OnPropertyChanged("Registration");
            }
        }

        bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                _isRunning = value;
                OnPropertyChanged("IsRunning");
            }
        }

        public ICommand CheckCommand => new Command(async () => await CheckAsync());

        public CheckPageViewModel()
        {
            service = new AirtableService();
        }

        private async Task CheckAsync()
        {
            IsRunning = true;
            var list = await service.GetAllRecords();
            var result = list.Select(m => m.Matricula);            

            if (!string.IsNullOrEmpty(Registration))
            {
                if (result.Contains(int.Parse(Registration)))
                {
                    var checkId = await service.CheckRegistration(Registration);
                    IsRunning = false;
                    if (!checkId)
                    {
                        IsRunning = false;
                        await Shell.Current.DisplayAlert("Alerta", "Já existe cadastro para essa matricula", "Ok");
                        return;
                    }
                    await PopupNavigation.PushAsync(new CheckPopupPage(Registration));                    
                }
                else
                {
                    IsRunning = false;
                    await Shell.Current.DisplayAlert("Aviso", "Matrícula não encontrada. Aguarde atualização", "Ok");
                }

            }
            else
            {
                IsRunning = false;
                await Shell.Current.DisplayAlert("Aviso", "Digite sua matricula", "Ok");
            }               
        }
    }
}