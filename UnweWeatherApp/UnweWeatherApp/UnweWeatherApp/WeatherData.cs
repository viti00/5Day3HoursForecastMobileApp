using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using UnweWeatherApp.ViewModels;

namespace UnweWeatherApp
{
    public class WeatherDataList
    {
        [JsonProperty("list")]
        public List<WeatherData> WeatherDataL { get; set; }
        [JsonProperty("city")]
        public City City { get; set; }
    }

    public class WeatherData
    {
        [JsonProperty("dt")]
        public long UnixTimeStamp { get; set; }
        [JsonProperty("main")]
        public Main Main { get; set; }
        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; }
        [JsonProperty("clouds")]
        public Clouds Clouds { get; set; }
        [JsonProperty("wind")]
        public Wind Wind { get; set; }
        [JsonProperty("sys")]
        public Sys Sys { get; set; }
        [JsonProperty("dt_txt")]
        public string DateText { get; set; }
    }

    public class City
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("sunrise")]
        public long Sunrise { get; set; }

        [JsonProperty("sunset")]
        public long Sunset { get; set; }

        public long DailyDuration
        {
            get
            {
                return Sunset - Sunrise;
            }
        }
    }

    public class Main
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }
        [JsonProperty("pressure")]
        public long Pressure { get; set; }

        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }

        [JsonProperty("humidity")]
        public long Humidity { get; set; }
        [JsonProperty("temp_min")]
        public double TempMin { get; set; }
        [JsonProperty("temp_max")]
        public double TempMax { get; set; }
    }

    public class Clouds
    {
        [JsonProperty("all")]
        public long All { get; set; }
    }

    public class Wind
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }
        [JsonProperty("deg")]
        public long Degree { get; set; }
    }

    public class Sys
    {
        [JsonProperty("pod")]
        public string PartOfDay { get; set; }
    }

    public class Weather
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("main")]
        public string Main { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
    }

}
