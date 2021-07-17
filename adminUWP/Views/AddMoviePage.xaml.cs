using adminUWP.Models;
using adminUWP.Services;
using ImageToArray;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace adminUWP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMoviePage : ContentPage
    {
        private MediaFile file;
        public AddMoviePage()
        {
            InitializeComponent();
        }
        private async void TapPickImage_Tapped(object sender, EventArgs e)
        {

            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Sorry", "Your device doesn't support this feature", "OK");
                return;
            }

            file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null)
                return;


            ImgMovie.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
        }

        private async void ImgSave_Tapped(object sender, EventArgs e)
        {
            //var validation = IsAfieldEmpty();
            //if (validation)
            //{
            //    await DisplayAlert("Error", "Favor de no dejar un campo vacio", "Ok");
            //}
            //else
            //{
            try
            {
                var imageArray = FromFile.ToArray(file.GetStream());
                var movie = new Movie()
                {
                    Name = EntMovieName.Text,
                    Description = EdtDescription.Text,
                    Language = EntLanguage.Text,
                    Duration = EntDuration.Text,
                    PlayingDate = EntPlayingDate.Date.ToString(),
                    PlayingDate2 = EntPlayingDate2.Date.ToString(),
                    PlayingDate3 = EntPlayingDate3.Date.ToString(),
                    PlayingDate4 = EntPlayingDate4.Date.ToString(),
                    PlayingDate5 = EntPlayingDate5.Date.ToString(),
                    PlayingTime = EntPlayingTime.Text,
                    PlayingTime2 = EntPlayingTime2.Text,
                    PlayingTime3 = EntPlayingTime3.Text,
                    PlayingTime4 = EntPlayingTime4.Text,
                    PlayingTime5 = EntPlayingTime5.Text,
                    TicketPrice = 150,
                    Rating = Convert.ToDouble(EntRating.Text),
                    Genre = EntGenre.Text,
                    TrailorUrl = EntTrailorUrl.Text,
                    ImageArray = imageArray
                };
                var response = await ApiService.AddMovie(file, movie);
                if (response)
                {
                    await DisplayAlert("Alerta", "Pelicula agregada correctamente", "Ok");
                    await Navigation.PopModalAsync();
                }
                return;
                

            }
            catch (Exception x)
            {
                await DisplayAlert("Oops", x.Message, "Cancel");
                return;
            }


        }
        private bool IsAfieldEmpty()
        {
            if (((string.IsNullOrEmpty(EntMovieName.Text) || string.IsNullOrEmpty(EdtDescription.Text))
                || (string.IsNullOrEmpty(EntLanguage.Text) || string.IsNullOrEmpty(EntDuration.Text))
                || (string.IsNullOrEmpty(EntLanguage.Text) || string.IsNullOrEmpty(EntPlayingDate.Date.ToString()))
                || string.IsNullOrEmpty(EntPlayingTime.Text)))
            {
                return true;
            }
            else { return false; }
        }

        private void ImgBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Alerta", "Realmente desea agregar otra funcion?", "Si", "No");
                if (result)
                {
                    EntPlayingDate2.IsEnabled = true;
                }
                else
                {
                    return;
                }
            });

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Alerta", "Realmente desea agregar otra funcion?", "Si", "No");
                if (result)
                {
                    EntPlayingTime2.IsEnabled = true;
                }
                else
                {
                    return;
                }
            });
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Alerta", "Realmente desea agregar otra funcion?", "Si", "No");
                if (result)
                {
                    EntPlayingDate3.IsEnabled = true;
                }
                else
                {
                    return;
                }
            });
        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Alerta", "Realmente desea agregar otra funcion?", "Si", "No");
                if (result)
                {
                    EntPlayingTime3.IsEnabled = true;
                }
                else
                {
                    return;
                }
            });
        }

        private void Button_Clicked_4(object sender, EventArgs e)
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Alerta", "Realmente desea agregar otra funcion?", "Si", "No");
                if (result)
                {
                    EntPlayingDate4.IsEnabled = true;
                }
                else
                {
                    return;
                }
            });
        }

        private void Button_Clicked_5(object sender, EventArgs e)
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Alerta", "Realmente desea agregar otra funcion?", "Si", "No");
                if (result)
                {
                    EntPlayingTime4.IsEnabled = true;
                }
                else
                {
                    return;
                }
            });
        }

        private void Button_Clicked_6(object sender, EventArgs e)
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Alerta", "Realmente desea agregar otra funcion?", "Si", "No");
                if (result)
                {
                    EntPlayingDate5.IsEnabled = true;
                }
                else
                {
                    return;
                }
            });
        }

        private void Button_Clicked_7(object sender, EventArgs e)
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Alerta", "Realmente desea agregar otra funcion?", "Si", "No");
                if (result)
                {
                    EntPlayingTime5.IsEnabled = true;
                }
                else
                {
                    return;
                }
            });
        }
    }
}
