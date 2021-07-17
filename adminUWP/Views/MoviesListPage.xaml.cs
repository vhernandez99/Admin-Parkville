using adminUWP.Models;
using adminUWP.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace adminUWP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoviesListPage : ContentPage
    {
        public ObservableCollection<MovieList> MoviesCollection;
        private int pageNumber = 0;
        public MoviesListPage()
        {
            InitializeComponent();
            MoviesCollection = new ObservableCollection<MovieList>();
            GetMovies();
        }
        private async void GetMovies()
        {
            pageNumber++;
            var movies = await ApiService.GetAllMovies(pageNumber, 50);
            foreach (var movie in movies)
            {
                MoviesCollection.Add(movie);
            }
            CvMovies.ItemsSource = MoviesCollection;
        }

        private void CvMovies_RemainingItemsThresholdReached(object sender, EventArgs e)
        {
            GetMovies();
        }

        private void ImgBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void CvMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSelection = e.CurrentSelection.FirstOrDefault() as MovieList;
            if (currentSelection == null) return;
            var result = await DisplayAlert("Alert", "Desea eliminar esta pelicula?", "Si", "No");
            if (result)
            {
                var response = await ApiService.DeleteMovie(currentSelection.Id);
                if (response == false) return;
                MoviesCollection.Clear();
                pageNumber = 0;
                GetMovies();
            }

            ((CollectionView)sender).SelectedItem = null;
        }

        private void ImgAdd_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddMoviePage());
        }
    }
}