using adminUWP.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace adminUWP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReservationDetailPage : ContentPage
    {
        public int reservId;
        public ReservationDetailPage(int reservationId)
        {
            InitializeComponent();
            GetReservationDetail(reservationId);
        }

        private async void GetReservationDetail(int reservationId)
        {
            var response=await ApiService.GetReservationDetail(reservationId);
            reservId = response.Id;
            LblReservationId.Text = response.Id.ToString();
            //LblReservationTime.Text = response.ReservationTime.ToString("MMMM d, yyyy HH:mm");
            LblCustomerName.Text = response.CustomerName;
            LblMovieName.Text = response.MovieName;
            LblEmail.Text = response.Email;
            LblPrice.Text = response.Price + "$";
            LblQty.Text = response.Qty.ToString();
            LblPlayingDate.Text = response.PlayingDate.ToString("MMMM d, yyyy");
            LblPlayingTime.Text = response.PlayingTime.ToString("HH:mm");
            LblPhone.Text = response.Phone;
            LblPagado.Text = response.IsPaid;
            LblUsado.Text = response.IsUsed;
        }
        private void ImgBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Alerta", "Realmente desea cambiar esta reservacion a pagada?", "Si", "No");
                if (result)
                {
                   string isPaid = "Si";
                   await ApiService.UpdatePaidTicket(reservId, isPaid);
                }
                else
                {
                    return;
                }
            });
        }
    }
}