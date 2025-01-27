using EventManagementMobile.Services;

namespace EventManagementMobile
{
    public partial class App : Application
    {
        public App(ApiService apiService)
        {
            InitializeComponent();

            //// Controleer of gebruikersgegevens zijn opgeslagen
            //var username = SecureStorage.GetAsync("Username").Result;
            //var password = SecureStorage.GetAsync("Password").Result;

            //if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            //{
            //    // Navigeren naar de MainPage als de gebruiker al is ingelogd
            //    MainPage = new NavigationPage(new MainPage());
            //}
            //else
            //{
            //    // Toon de LoginPage
            //    MainPage = new NavigationPage(new LoginPage(apiService));
            //}

            MainPage = new NavigationPage(new EventsPage());
        }
    }
}