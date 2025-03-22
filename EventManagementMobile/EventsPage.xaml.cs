using EventManagementMobile.Models;
using System.Net.Http.Json;

namespace EventManagementMobile;

public partial class EventsPage : ContentPage
{
    private readonly HttpClient _httpClient;

    public EventsPage()
    {
        InitializeComponent();
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://10.0.2.2:5173/api/")
        };
        LoadEvents();
    }

    private async void LoadEvents()
    {
        try
        {
            var events = await _httpClient.GetFromJsonAsync<List<Event>>("Events");
            if (events != null)
            {
                EventsListView.ItemsSource = events;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load events: {ex.Message}", "OK");
        }
    }
}

