using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FS_App_R
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        //Personal Interno!
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserInfo(true));
        }

        //Personal Externo!
        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserInfo(false));
        }
    }
}
