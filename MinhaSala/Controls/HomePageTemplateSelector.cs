using System;
using MinhaSala.Models;
using Xamarin.Forms;

namespace MinhaSala.Controls
{
    public class HomePageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate BlueTemplate { get; set; }
        public DataTemplate GreenTemplate { get; set; }
        public DataTemplate PinkTemplate { get; set; }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is AirtableData)
            {
                if (((AirtableData)item).Sala == "EAD")
                    return BlueTemplate;
                else if (((AirtableData)item).Sala == "REMOTO")
                    return PinkTemplate;
            }
                
            

            return GreenTemplate;
        }
    }
}

