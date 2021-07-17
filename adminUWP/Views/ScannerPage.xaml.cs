using adminUWP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace adminUWP.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ScannerPage : ContentPage
{
    public int reservationId;
    public ScannerPage()
    {
        InitializeComponent();
    }
        private void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                reservationId = Int32.Parse(result.Text);
                ScanReservation(reservationId);
            });
        }
        
        private async void ScanReservation(int reservationId)
        {
            var reservation = await ApiService.GetReservationDetail(reservationId);
            id.Text = reservation.Id.ToString();
            Fecha.Text = reservation.PlayingDate.ToString();
            pelicula.Text = reservation.MovieName;
            pagado.Text = reservation.IsPaid;
            usado.Text = reservation.IsUsed;
        }

        private void Limpiar_Clicked(object sender, EventArgs e)
        {
            id.Text = "";
            Fecha.Text = "";
            pelicula.Text = "";
            pagado.Text = "";
            usado.Text = "";
        }

        private async void Actualizar_Clicked(object sender, EventArgs e)
        {
            string isUsed = "Si";
            var response = await ApiService.UpdateUsedTicket(reservationId,isUsed);
        }
    }
}