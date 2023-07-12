using System;
using System.ComponentModel;
using System.Threading.Tasks;
using MinhaSala.Models;
using Xamarin.Forms;

namespace MinhaSala.ViewModels
{
    public class DetailsPageViewModel : BaseViewModel
    {
        AirtableData _item;
        public AirtableData Item
        {
            get => _item;
            set
            {
                _item = value;
                OnPropertyChanged("Item");
            }
        }
        public DetailsPageViewModel(AirtableData airtableData)
        {
            Item = new AirtableData();

            var data = airtableData;
            Item.Curso = data.Curso;
            Item.Dia = data.Dia;
            Item.Disciplina = data.Disciplina;
            Item.HoraFim = data.HoraFim;
            Item.HoraInicio = data.HoraInicio;
            Item.Matricula = data.Matricula;
            Item.Sala = data.Sala;
            Item.Unidade = data.Unidade;
        }
    }
}

