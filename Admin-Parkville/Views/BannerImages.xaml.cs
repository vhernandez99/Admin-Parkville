using Admin_Parkville.Models;
using Admin_Parkville.Services;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Admin_Parkville.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BannerImages : ContentPage
    {
        public ObservableCollection<BannerImage> BannerImagesCollection;
        public BannerImages()
        {
            InitializeComponent();
            BannerImagesCollection = new ObservableCollection<BannerImage>();
            GetAllBannerImages();
        }

        public async void GetAllBannerImages()
        {
            var response = await ApiService.GetAllBannerImages();
            foreach (var bannerImgs in response)
            {
                BannerImagesCollection.Add(bannerImgs);
            }
            cvBannerImages.ItemsSource = BannerImagesCollection;
        }
        private void cvBannerImages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as BannerImage;
            if (currentSelection == null) return;
            Navigation.PushModalAsync(new AddBannerImage(currentSelection.Id));
            ((CollectionView)sender).SelectedItem = null;
        }

        private void ImgBack_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}