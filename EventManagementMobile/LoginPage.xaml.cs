using EventManagementMobile;
using EventManagementMobile.Services;
using Microsoft.Maui.Controls;
using EventManagementMobile.Models;

namespace EventManagementMobile
{
    public partial class LoginPage : ContentPage
    {
        private readonly ApiService _apiService;

        public LoginPage(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageLabel.Text = "Please enter username and password.";
                return;
            }

            bool loginSuccess = await _apiService.LoginAsync(username, password);

            if (loginSuccess)
            {
                using (var db = new LocalDatabase())
                {
                    db.Users.Add(new User { Username = username, Password = password }); 
                    db.SaveChanges();
                }

                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                MessageLabel.Text = "Invalid username or password.";
            }
        }
    }

}

