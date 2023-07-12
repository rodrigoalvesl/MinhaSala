using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MinhaSala.Models;
using MinhaSala.Services;
using MinhaSala.Views;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.UI.Views;

namespace MinhaSala.ViewModels
{
    [QueryProperty(nameof(Registration), "Id")]
    public class HomePageViewModel : BaseViewModel
    {
        public AirtableService service;

        DetailsPageViewModel vmDetails;
        #region paramShell
        string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("Number");
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

        ObservableCollection<AirtableData> _list;
        public ObservableCollection<AirtableData> List
        {
            get => _list;
            set
            {
                _list = value;
                OnPropertyChanged("List");
            }
        }
        AirtableData _selectedItem;
        public AirtableData SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        LayoutState _currentState = LayoutState.Loading;
        public LayoutState CurrentState
        {
            get => _currentState;
            set
            {
                if (_currentState != value)
                {
                    _currentState = value;
                    OnPropertyChanged(nameof(CurrentState));
                }
            }
        }

        bool _isLoadingMore;
        public bool IsLoadingMore
        {
            get => _isLoadingMore;
            set
            {
                if (_isLoadingMore != value)
                {
                    _isLoadingMore = value;
                    OnPropertyChanged(nameof(IsLoadingMore));
                }
            }
        }

        public ICommand DetailsCommand => new Command(async () => await GoToDetailsAsync());

        public HomePageViewModel()
        {
            service = new AirtableService();
            Task.Run(async () => await ListData());
        }

        private async Task ListData()
        {
            CurrentState = LayoutState.Loading;
            await Task.Delay(2000);
            var result = await service.GetAllRecords();
            var resultFilter = result.Where(m => m.Matricula == int.Parse(Id)).OrderBy(s => s.Sala == "EAD").ThenBy(d => d.Disciplina);
            List = new ObservableCollection<AirtableData>(resultFilter);
            CurrentState = LayoutState.None;
        }

        //Para utilizar, trocar a propiedade SelectionMode do CollectionView para "Single"
        public async Task GoToDetailsAsync()
        {
            AirtableData airtableData = new AirtableData();
            airtableData.Curso = SelectedItem.Curso;
            airtableData.Dia = SelectedItem.Dia;
            airtableData.Disciplina = SelectedItem.Disciplina;
            airtableData.HoraFim = SelectedItem.HoraFim;
            airtableData.HoraInicio = SelectedItem.HoraInicio;
            airtableData.Matricula = SelectedItem.Matricula;
            airtableData.Sala = SelectedItem.Sala;
            airtableData.Unidade = SelectedItem.Unidade;

            await App.Current.MainPage.Navigation.PushAsync(new DetailsPage(airtableData));

        }
    }
}

