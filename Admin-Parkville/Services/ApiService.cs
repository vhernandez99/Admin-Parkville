using Admin_Parkville.Models;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UnixTimeStamp;
using Xamarin.Essentials;

namespace Admin_Parkville.Services
{
    public static class ApiService
    {
        private static Uri FireBasePushNotificationsURL = new Uri("https://fcm.googleapis.com/fcm/send");
        private static string ServerKey = "AAAAHQm_kQ4:APA91bHplt9TGV6L-gtOrCDiouB-NdRBlSprEgiGn5XX7TjVmBevEPYAlSXHPWwFyAOf1AbIJnP_w-wfDZqSaJQxrEJ0rX3p1w9nK0GxEFVk_EaHtuLCQTS9gXn7RvFD4I3KNdInaxsZ";
        public static async Task<bool> RegisterUser(string name, string email, string password,string phoneNumber)
        {
            var register = new Register()
            {
                Name = name,
                Email = email,
                Password = password,
                PhoneNumber=phoneNumber 
            };
            var httpClient = new HttpClient();
            var jsonRegister = JsonConvert.SerializeObject(register);
            var content = new StringContent(jsonRegister, Encoding.UTF8, "application/json");
            var ApiResponse = await httpClient.PostAsync(AppSettings.ApiUrl + "api/users/Register", content);
            if (!ApiResponse.IsSuccessStatusCode) return false;
            return true;
        }
        

        public static async Task<bool> Login(string email, string password)
        {
            var login = new Login()
            {
                Email = email,
                Password = password
            };
            var httpclient = new HttpClient();
            var jsonRegister = JsonConvert.SerializeObject(login);
            var content = new StringContent(jsonRegister, Encoding.UTF8, "application/json");
            var ApiResponse = await httpclient.PostAsync(AppSettings.ApiUrl + "api/users/login", content);
            if (!ApiResponse.IsSuccessStatusCode) return false;
            var jsonresult = await ApiResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Token>(jsonresult);
            Preferences.Set("accessToken", result.Access_token);
            Preferences.Set("userId", result.User_id);
            Preferences.Set("userName", result.User_Name);
            Preferences.Set("tokenExpirationTime", result.Expiration_Time);
            Preferences.Set("currentTime", UnixTime.GetCurrentTime());
            return true;
        }

        public static async Task<bool> UpdateUsedTicket(int id, string isUsed)
        {
            var reserv = new ReservationDetail
            {
                IsUsed = isUsed
            };
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var jsonRegister = JsonConvert.SerializeObject(reserv);
            var content = new StringContent(jsonRegister, Encoding.UTF8, "application/json");
            var ApiResponse = await httpClient.PutAsync(AppSettings.ApiUrl + "api/Reservations/UpdateIfTicketIsUsed/" + id, content);
            if (!ApiResponse.IsSuccessStatusCode) return false;
            return true;
        }

        
        public static async Task<List<MovieList>> GetAllMovies(int pageNumber, int pageSize)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + string.Format("api/movies/AllMovies?sort=asc&pageNumber={0}&pageSize={1}", pageNumber, pageSize));
            return JsonConvert.DeserializeObject<List<MovieList>>(response);
        }

        public static async Task<List<BannerImage>> GetAllBannerImages()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + string.Format("api/movies/AllBannerImages"));
            return JsonConvert.DeserializeObject<List<BannerImage>>(response);
        }

        public static async Task<bool> DeleteMovie(int movieId)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            var movie = new MovieList
            {
                Status = 0
            };

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var jsonRegister = JsonConvert.SerializeObject(movie);
            var content = new StringContent(jsonRegister, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(AppSettings.ApiUrl + "api/movies/DeleteMovie/" + movieId,content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }
        public static async Task<bool> UpdatePaidTicket(int id, string isPaid)
        {
            var reserv = new ReservationDetail
            {
                IsPaid = isPaid
            };
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var jsonRegister = JsonConvert.SerializeObject(reserv);
            var content = new StringContent(jsonRegister, Encoding.UTF8, "application/json");
            var ApiResponse = await httpClient.PutAsync(AppSettings.ApiUrl + "api/Reservations/UpdateIfTicketIsPaid/" + id, content);
            if (!ApiResponse.IsSuccessStatusCode) return false;
            return true;
        }
        public static async Task<List<Reservation>> GetAllReservations ()
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/reservations/GetReservations");
            return JsonConvert.DeserializeObject<List<Reservation>>(response);
        }
        public static async Task<ReservationDetail> GetReservationDetail(int reservationId)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl+"api/reservations/GetReservationDetail/"+reservationId);
            return JsonConvert.DeserializeObject<ReservationDetail>(response);
        }


        
        public static async Task<bool> AddMovie(MediaFile mediaFile,Movie movie)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var content = new MultipartFormDataContent
            {
                {new StringContent(movie.Name),"Name" },
                {new StringContent(movie.Description),"Description" },
                {new StringContent(movie.Language),"Language" },
                {new StringContent(movie.Duration),"Duration" },
                {new StringContent(movie.PlayingDate),"PlayingDate" },
                {new StringContent(movie.PlayingTime),"PlayingTime" },
                {new StringContent(movie.TicketPrice.ToString()),"TicketPrice" },
                {new StringContent(movie.Rating.ToString()),"Rating" },
                {new StringContent(movie.Genre),"Genre" },
                {new StringContent(movie.TrailorUrl),"TrailorUrl" },
            };
            content.Add(new StreamContent(new MemoryStream(movie.ImageArray)), "Image", mediaFile.Path);
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/movies/AddMovie", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }
        public static async Task<bool> UpdateBannerImage(MediaFile mediaFile, int id, BannerImage bannerImage)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var content = new MultipartFormDataContent
            {
                
            };
            content.Add(new StreamContent(new MemoryStream(bannerImage.ImageArray)), "Image", mediaFile.Path);
            var response = await httpClient.PutAsync(AppSettings.ApiUrl + "api/movies/UpdateImageBanner/"+id, content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        


        public static async Task<bool> AddBannerImage(MediaFile mediaFile, BannerImage bannerImage)
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var content = new MultipartFormDataContent
            {
                
            };
            content.Add(new StreamContent(new MemoryStream(bannerImage.ImageArray)), "Image", mediaFile.Path);
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/movies/AddMovie", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }
        

        public static async Task<List<TokensFirebase>> GetAllTokens()
        {
            await TokenValidator.CheckTokenValidity();
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/users/GetAllTokens");
            var des=JsonConvert.DeserializeObject<List<TokensFirebase>>(response);
            return des;

        }
        public static async Task<bool> SendPushNotification(string title, string body, object data)
        {
            var usersTokens = await ApiService.GetAllTokens();
            var deviceTokens = usersTokens.Select(x => x.Token).ToArray();
            bool sent = false;
                var messageInformation = new Message()
                {
                    notification = new Notification()
                    {
                        title = title,
                        body = body
                    },
                    data = data,
                    registration_ids = deviceTokens
                };
                string jsonMessage = JsonConvert.SerializeObject(messageInformation);
                var request = new HttpRequestMessage(HttpMethod.Post, FireBasePushNotificationsURL);
                request.Headers.TryAddWithoutValidation("Authorization", "key=" + ServerKey);
                request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
                HttpResponseMessage result;
                using (var client = new HttpClient())
                {
                    result = await client.SendAsync(request);
                    sent = sent && result.IsSuccessStatusCode;
                }
            return sent;
        }
    }
    public static class TokenValidator
    {
        public static async Task CheckTokenValidity()
        {
            var expirationTime=Preferences.Get("tokenExpiration", 0);
            Preferences.Set("currentTime", UnixTime.GetCurrentTime());
            var currentTime = Preferences.Get("currentTime", 0);
            if (expirationTime < currentTime)
            {
                var email=Preferences.Get("email", string.Empty);
                var password = Preferences.Get("password", string.Empty);
                await ApiService.Login(email,password);
            }
        }
    }
}   