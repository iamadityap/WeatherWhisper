using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherWhisper
{
  class WeatherService
  {
    private readonly string API_KEY = "af4aebb78e09001fe7aba01837d3e2f5";
    private readonly string OneCallUrl = "https://api.openweathermap.org/data/2.5/onecall?";
    private readonly string GeoCodingUrl = "https://api.openweathermap.org/geo/1.0/direct?";

    private string lang = "en";
    private string units = "metric";

    private readonly HttpClient _httpClient;

    public WeatherService()
    {
      _httpClient = new HttpClient();
      _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    public async Task<string> GetWeatherAsync(string location)
    {
      var LatLong = await GetLocationCoordinates(location);

      string apiUrl = $"{OneCallUrl}key={API_KEY}&lat={LatLong.Item1}&lon={LatLong.Item2}&units={units}&lang={lang}";

      HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

      if (response.IsSuccessStatusCode)
      {
        string result = await response.Content.ReadAsStringAsync();
        return result;
      }

      // Handle error cases if needed
      return null;
    }

    public async Task<(double Latitude, double Longitude)> GetLocationCoordinates(string location)
    {
      string url = $"{GeoCodingUrl}q={location}&limit=1&appid={API_KEY}";

      using (HttpClient client = new HttpClient())
      {
        HttpResponseMessage response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
          string responseContent = await response.Content.ReadAsStringAsync();
          dynamic data = JsonConvert.DeserializeObject<dynamic>(responseContent);

          if (data.Count > 0)
          {
            var firstResult = data[0];
            double latitude = firstResult.lat;
            double longitude = firstResult.lon;
            
            string message = $"Latitude: {latitude}, Longitude: {longitude}";
            System.Console.WriteLine(message);

            return (latitude, longitude);
          }
        }
      }

      return (0, 0);
    }
  }
}
