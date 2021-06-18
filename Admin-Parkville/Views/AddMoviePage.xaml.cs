using Admin_Parkville.Models;
using Admin_Parkville.Services;
using ImageToArray;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
                    PlayingTime = EntPlayingTime.Text,
                    TicketPrice = Convert.ToInt32(EntTicketPrice.Text),
                    Rating = Convert.ToDouble(EntRating.Text),
                    Genre = EntGenre.Text,
                    TrailorUrl = EntTrailorUrl.Text,
                    ImageArray = imageArray
                };
                var response = await ApiService.AddMovie(file, movie);
                await DisplayAlert("Alerta", "Pelicula agregada correctamente", "Aceptar");
            }
            catch (Exception)
            {
                await DisplayAlert("Oops", "Hubo un error al subir la pelicula", "Cancel");
                return;
            }
            
            
        }

        private void ImgBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
