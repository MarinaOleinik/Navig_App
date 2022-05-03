using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Navig_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Prop_Page : ContentPage
    {
        Entry toode_entry;
        Button salvesta, naita;
        Label toode_list;
        public Prop_Page()
        {
            toode_entry = new Entry {  };
            salvesta = new Button { Text = "Salvesta" };
            naita = new Button { Text = "Näita" };
            toode_list = new Label { };
            StackLayout st = new StackLayout { Children = { toode_entry, salvesta,toode_list,naita } };
            Content = st;
            salvesta.Clicked += Salvesta_Clicked;
            naita.Clicked += Naita_Clicked;
        }
        protected override void OnAppearing()
        {
            object toode="";
            if (App.Current.Properties.TryGetValue("toode",out toode))
            {
                // выполняем действия, если в словаре есть ключ "toode"
                toode_list.Text = (string)App.Current.Properties["toode"];
            }
            base.OnAppearing();
        }
        private void Salvesta_Clicked(object sender, EventArgs e)
        {
            string value = toode_entry.Text;
            App.Current.Properties.Add("toode", value);
        }
        private void Naita_Clicked(object sender, EventArgs e)
        {
            toode_list.Text= (string)App.Current.Properties["toode"];
        }
    }
}