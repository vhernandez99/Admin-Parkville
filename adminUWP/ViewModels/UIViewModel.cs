using adminUWP.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace adminUWP.ViewModels
{
    public static class UIViewModel
    {
        

        public static async Task GoToLoginPageAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
        }
        public static async Task GoToSignupPageAsync()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
            
        }
    }
}
