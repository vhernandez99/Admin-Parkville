using Admin_Parkville.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Admin_Parkville
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Device.SetFlags(new string[] { "MediaElement_Experimental" });
            var accessToken = Preferences.Get("accessToken", string.Empty);
            if (string.IsNullOrEmpty(accessToken))
            {
                MainPage = new SignupPage();
            }
            else
            {
                MainPage = new NavigationPage(new HomePage());
            }

            //MainPage = new HomeTabbed();

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
