using Newtonsoft.Json;
using System.ComponentModel;

namespace WeatherWhisper
{
  class LocationCoordinates : INotifyPropertyChanged
  {
    [JsonProperty("lon")]
    private double lon { get; set; }
    public double Lon
    {
      get { return lon; }
      set
      {
        if (lon != value)
        {
          lon = value;
          OnPropertyChanged(nameof(Lon));
        }
      }
    }

    [JsonProperty("lat")]
    private double lat { get; set; }
    public double Lat
    {
      get { return lat; }
      set
      {
        if (lat != value)
        {
          lat = value;
          OnPropertyChanged(nameof(Lat));
        }
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
