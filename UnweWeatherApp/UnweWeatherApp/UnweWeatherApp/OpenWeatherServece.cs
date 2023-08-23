using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static UnweWeatherApp.GlobalVariables;

namespace UnweWeatherApp
{
    public class OpenWeatherServece
    {
        HttpClient _client;
        public OpenWeatherServece()
        {
            _client = new HttpClient();
        } 

        public async Task<List<WeatherData>> GetWeatherData(string query)
        {
            List<WeatherData> weatherData = null;
            try
            {
                var response = await _client.GetAsync(query);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var weatherDataList = JsonConvert.DeserializeObject<WeatherDataList>(content);

                    weatherData = weatherDataList.WeatherDataL;
                    cityVariable = weatherDataList.City;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\tERROR {0}", ex.Message);
            }
            return weatherData;
        }
    }
}
