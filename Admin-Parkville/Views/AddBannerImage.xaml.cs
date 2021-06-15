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
public partial class AddBannerImage : ContentPage
{
    private MediaFile file;
    public int id;
    public AddBannerImage(int bannerImageId)
    {
        InitializeComponent();
        id = bannerImageId;
    }
        //borrar
        private async void ImgSave_Tapped(object sender, EventArgs e)
        {
            var imageArray = FromFile.ToArray(file.GetStream());
            var bannerImage = new BannerImage()
            {
                ImageArray = imageArray
            };
            var response = await ApiService.UpdateBannerImage(file, id, bannerImage);

            if (!response)
            {
                await DisplayAlert("Oops", "Hubo un error al actualizar la foto, intente de nuevo", "Cancel");
            }
            else
            {
                await DisplayAlert("Alerta", "Imagen actualizada correctamente", "Aceptar");
                await Navigation.PopModalAsync();
            }
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
    }
}