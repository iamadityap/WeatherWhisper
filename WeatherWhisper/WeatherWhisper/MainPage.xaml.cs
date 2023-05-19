using System;
using Xamarin.Forms;

namespace WeatherWhisper
{
  public partial class MainPage : ContentPage
  {
    private WeatherService weatherService;
    private LocationCoordinates coords;
    public bool IsFetchingWeather;

    public MainPage()
    {
      InitializeComponent();

      weatherService = new WeatherService();

      coords = new LocationCoordinates
      {
        Lat = 0.0,
        Lon = 0.0
      };

      latCoordinates.Text = coords.Lat.ToString();
      lonCoordinates.Text = coords.Lon.ToString();

      BindingContext = coords;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
      string locationInputText = locationInput.Text;

      if (string.IsNullOrWhiteSpace(locationInputText))
      {
        await DisplayAlert("Location", "Please enter a location.", "OK");
        return;
      }


      try
      {
        IsFetchingWeatherData.IsRunning = true;

        var results = await weatherService.GetLocationCoordinates(locationInputText);
        latCoordinates.Text = results.Latitude.ToString();
        lonCoordinates.Text = results.Longitude.ToString();
      }
      finally
      {
        IsFetchingWeatherData.IsRunning = false;
      }
    }
  }
}
