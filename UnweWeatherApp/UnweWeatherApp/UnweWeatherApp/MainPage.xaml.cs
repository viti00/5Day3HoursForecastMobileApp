using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnweWeatherApp.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using static UnweWeatherApp.GlobalMethods;
using static UnweWeatherApp.GlobalVariables;

namespace UnweWeatherApp
{
   
    public partial class MainPage : ContentPage
    {
        WeatherDataListViewModel viewModel = null;
        readonly OpenWeatherServece _openWeatherService;
        public MainPage()
        {
            InitializeComponent();
            _openWeatherService = new OpenWeatherServece();

            GetWeatherWithGeoLoaction();
        }

        string GenerateRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"?q={cityEntry.Text}";
            requestUri += "&units=metric"; // or units=metric
            requestUri += $"&APPID={Constants.OpenWeatherMapAPIKey}";
            return requestUri;
        }

        string GenerateRequestUriGeo(string endpoint, double lat, double longt)
        {
            string requestUri = endpoint;
            requestUri += $"?lat={lat}";
            requestUri += $"&lon={longt}";
            requestUri += "&units=metric"; // or units=metric
            requestUri += $"&APPID={Constants.OpenWeatherMapAPIKey}";
            return requestUri;
        }

        public async void OnGetWeatherButtonClicked(object sendere, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(cityEntry.Text))
            {
                List<WeatherData> weatherData = await _openWeatherService.GetWeatherData(GenerateRequestUri(Constants.OpenWeatherMapEndpoint));
                viewModel = new WeatherDataListViewModel(weatherData);
                BindingContext = viewModel;
            }
        }

        public async void GetWeatherWithGeoLoaction()
        {
            var location = await Geolocation.GetLocationAsync();
            double lat = 0;
            double lon = 0;
            if (location != null)
            {
                lat = location.Latitude;
                lon = location.Longitude;
                List<WeatherData> weatherData = await _openWeatherService.GetWeatherData(GenerateRequestUriGeo(Constants.OpenWeatherMapEndpoint, lat, lon));
                viewModel = new WeatherDataListViewModel(weatherData);
                cityEntry.Text = cityVariable.Name.ToString();
                BindingContext = viewModel;
            }
        }

        private void OnFrameTapped(object sender, EventArgs e)
        {

            var frame = (Frame)sender;
            var frameModel = (WeatherDataViewModel)frame.BindingContext;

            var vm = new WeatherDataListViewModel(viewModel.FirstDataListForEachDay, GetCurrDayWeather(frameModel.Index));

            BindingContext = vm;
        }
    }
}
