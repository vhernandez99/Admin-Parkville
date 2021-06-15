using Admin_Parkville.Services;
using Admin_Parkville.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace  Admin_Parkville.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        

        private string _Username;
        public string Username
        {
            set
            {
                _Username = value;
                OnPropertyChanged();
            }
            get
            {
                return _Username;
            }
        }

        private string _Password;
        public string Password
        {
            set
            {
                _Password = value;
                OnPropertyChanged();
            }
            get
            {
                return _Password;
            }
        }
        private string _ConfirmPassword;
        public string ConfirmPassword
        {
            set
            {
                _ConfirmPassword = value;
                OnPropertyChanged();
            }
            get
            {
                return _ConfirmPassword;
            }
        }


        
        private string _Email;
        public string Email
        {
            set
            {
                _Email = value;
                OnPropertyChanged();
            }
            get
            {
                return _Email;
            }
        }
        private string _Phonenumber;
        public string Phonenumber
        {
            set
            {
                _Phonenumber = value;
                OnPropertyChanged();
            }
            get
            {
                return _Phonenumber;
            }
        }

        private bool _IsBusy;
        public bool IsBusy
        {
            set
            {
                _IsBusy = value;
                OnPropertyChanged();
            }
            get
            {
                return _IsBusy;
            }
        }

        public Command SignupCommand { get; set; }
        public Command LoginCommand { get; set; }
        public Command GoToLoginPage { get; set; }
        public Command GoToSignupPage { get; set; }

        public LoginViewModel()
        {
            SignupCommand = new Command(async () => await SignupCommandAsync());
            LoginCommand = new Command(async () => await LoginCommandAsync());
            GoToLoginPage = new Command(async () => await UIViewModel.GoToLoginPageAsync());
            GoToSignupPage = new Command(async () => await UIViewModel.GoToSignupPageAsync());
        }

        private async Task SignupCommandAsync()
        {
            if (IsBusy)
                return;
            if (Verification())
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Favor de verificar su informacion", "Aceptar");
                return;
            }
            try
            {
                IsBusy = true;
                var response = await ApiService.RegisterUser(Username, Email, Password, Phonenumber);
                IsBusy = false;

                if (response)
                {
                    await Application.Current.MainPage.DisplayAlert("Alerta", "Cuenta creada correctamente", "Aceptar");
                    await Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
                    IsBusy = false;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Correo ya registrado", "Aceptar");
                    IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }
        public bool Verification()
        {
            var verification = (string.IsNullOrEmpty(_Email) || string.IsNullOrEmpty(_Password) || string.IsNullOrEmpty(_ConfirmPassword) || _ConfirmPassword!=_Password);
            return verification;
        }

        private async Task LoginCommandAsync()
        {
            var response = await ApiService.Login(Email, Password);
            Preferences.Set("email", Email);
            Preferences.Set("password", Password);
            if (response)
            {
                Application.Current.MainPage = new NavigationPage(new HomePage());
                
            } 
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Usuario o contraseña incorrecta", "Aceptar");
            }
        }



        
        
    }
}
