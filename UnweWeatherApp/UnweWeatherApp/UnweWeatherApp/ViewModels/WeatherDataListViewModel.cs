using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using static UnweWeatherApp.GlobalMethods;
using static UnweWeatherApp.GlobalVariables;

namespace UnweWeatherApp.ViewModels
{
    public class WeatherDataListViewModel
    {
        public CurrDayWeatherDataViewModel CurrDayWeatherDataList { get; set; }
        public ObservableCollection<WeatherDataViewModel> FirstDataListForEachDay { get; set; }

        public WeatherDataListViewModel(ObservableCollection<WeatherDataViewModel> firstDataListForEachDay, CurrDayWeatherDataViewModel currDayWeatherDataList)
        {
            this.CurrDayWeatherDataList = currDayWeatherDataList;
            this.FirstDataListForEachDay = firstDataListForEachDay;
        }
        public WeatherDataListViewModel(List<WeatherData> weatherDataList)
        {
           
            FirstDataListForEachDay = new ObservableCollection<WeatherDataViewModel>();


            List<WeatherData> firstDataListForEachDay = weatherDataList
                                                            .GroupBy(w => w.DateText.Substring(0, 10))
                                                            .Select(g => new WeatherData
                                                            {
                                                                UnixTimeStamp = g.First().UnixTimeStamp,
                                                                Main = new Main
                                                                {
                                                                    TempMin = g.Min(w => w.Main.TempMin),
                                                                    TempMax = g.Max(w => w.Main.TempMax)
                                                                },
                                                                Weather = new List<Weather>
                                                                {
                                                                    new Weather
                                                                    {
                                                                        Icon = g.OrderByDescending(w => w.Main.TempMax)
                                                                                .First()
                                                                                .Weather[0].Icon
                                                                    }
                                                                },
                                                                DateText = g.First().DateText
                                                            })
                                                            .ToList();
            InitCurrDayAllInfo(weatherDataList);
            int count = 0;

            CurrDayWeatherDataList = GetCurrDayWeather(count);

            foreach (var weatherData in firstDataListForEachDay)
            {
                FirstDataListForEachDay.Add(new WeatherDataViewModel(weatherData, count));

                count++;
            }
        }
    }

    public class WeatherDataViewModel
    {
        public int Index { get; set; }
        public string DayOfWeek { get; set; }
        public string WeatherIconUrl { get; set; }
        public string MinTemperature { get; set; }
        public string MaxTemperature { get; set; }
        public WeatherDataViewModel(WeatherData weatherData, int count)
        {
            Index = count;
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var forecastDate = dateTime.AddSeconds(weatherData.UnixTimeStamp).ToLocalTime();
            DayOfWeek = forecastDate.ToString("dddd");
            WeatherIconUrl = $"https://openweathermap.org/img/wn/{weatherData.Weather[0].Icon}.png";
            MinTemperature = $"{weatherData.Main.TempMin}°C";
            MaxTemperature = $"{weatherData.Main.TempMax}°C";
        }
    }

    public class CurrDayWeatherDataViewModel
    {
        public string DayName { get; set; }
        public string CurrentTemperature { get; set; }
        public string MinTemperature { get; set; }
        public string MaxTemperature { get; set; }
        public string FeelsLike { get; set; }
        public string Humidity { get; set; }

        public City City { get; set; }
        public List<HourlyForecastViewModel> HourlyData { get; set; }

        public CurrDayWeatherDataViewModel(string dayName, string currentTemperature, string minTemperature, string maxTemperature, string feelsLike,string humidity,List<HourlyForecastViewModel> hourlyForecasts)
        {
            DayName = dayName;
            CurrentTemperature = currentTemperature;
            MinTemperature = minTemperature;
            MaxTemperature = maxTemperature;
            FeelsLike = feelsLike;
            Humidity = humidity;
            City = cityVariable;
            HourlyData = hourlyForecasts;
        }
    }

    public class HourlyForecastViewModel
    {
        public string TimeRange { get; set; }
        public string MinTemperature { get; set; }
        public string MaxTemperature { get; set; }
        public string Icon { get; set; }
    }

}
