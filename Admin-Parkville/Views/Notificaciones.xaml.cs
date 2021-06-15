using Admin_Parkville.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Admin_Parkville.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Notificaciones : ContentPage
{
    public Notificaciones()
    {
        InitializeComponent();
    }

        private void Enviar_Clicked(object sender, EventArgs e)
        {
            GetTokens();
        }

        private async void GetTokens()
        {
            var title = TituloEntry.Text.ToString();
            var body = CuerpoEntry.Text.ToString();
            var data = new { action = "Play", userId = 5 };
            var sendPush = await ApiService.SendPushNotification(title,body,data);
            //usersTokens.ToArray();

        }
    }
}