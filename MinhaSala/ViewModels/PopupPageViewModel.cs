using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MinhaSala.Services;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace MinhaSala.ViewModels
{    
    public class PopupPageViewModel : BaseViewModel
    {
        public AirtableService service;

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

        DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }

        DateTime _maxDate;
        public DateTime MaxDate
        {
            get => _maxDate;
            set
            {
                _maxDate = value;
                OnPropertyChanged("MaxDate");
            }
        }
        string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("_id");
            }
        }

        public ICommand ValidationCommand => new Command(async () => await ValidateAsync());

    

        public PopupPageViewModel(string id)
        {
            service = new AirtableService();
            Date = DateTime.Now;
            MaxDate = DateTime.Today;
            Id = id;        
        }

        public async Task FilterRegistration(string id)
        {
            IsRunning = true;
            var result = await service.GetAllRecords();
            var list = result.ToList();
            var user = list.Find(m => m.Matricula == int.Parse(id));
            if (Date == user.DataNascimento)
            {
                IsRunning = false;
                await Shell.Current.DisplayAlert("Sucesso!", "Matricula encontrada, prossiga com cadastro", "Ok");
                await PopupNavigation.PopAsync();
                await Shell.Current.GoToAsync($"/SignUpPage?Id={id}");
            }
            else
            {
                IsRunning = false;
                await Shell.Current.DisplayAlert("Alerta", "Data de nascimento incompativel com matricula digitada, tente novamente", "Ok");
            }
        }

        private async Task ValidateAsync()
        {
            await FilterRegistration(Id);
        }
    }
}

