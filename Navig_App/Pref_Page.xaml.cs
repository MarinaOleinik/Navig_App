using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Navig_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Pref_Page : ContentPage
    {
        Entry toode_entry;
        Button salvesta, naita;
        Label toode_list;
        public Pref_Page()
        {
            toode_entry = new Entry { };
            salvesta = new Button { Text = "Salvesta" };
            toode_list = new Label { };
            StackLayout st = new StackLayout { Children = { toode_entry, salvesta, toode_list } };
            Content = st;
            salvesta.Clicked += Salvesta_Clicked;
        }
        protected override void OnAppearing()
        {
            toode_list.Text = Preferences.Get("toode", "ei ole sisestatud");// 
            base.OnAppearing();
        }
        private void Salvesta_Clicked(object sender, EventArgs e)
        {
            string value = toode_entry.Text;
            Preferences.Set("toode", value);//
            toode_list.Text = value;
        }
        
    }
}