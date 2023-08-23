using System;
using System.Collections.Generic;
using System.Linq;
using UnweWeatherApp.ViewModels;
using static UnweWeatherApp.GlobalVariables;

namespace UnweWeatherApp
{
    public static class GlobalMethods
    {
        public static void InitCurrDayAllInfo(List<WeatherData> weatherDataList)
        {
            foreach (var group in weatherDataList.GroupBy(w => w.DateText.Substring(0, 10)))
            {
                var hourlyForecasts = group.Select(w => new HourlyForecastViewModel
                {
                    TimeRange = $"{w.DateText.Substring(11, 2)}:00 - {(int.Parse(w.DateText.Substring(11, 2)) + 2)}:00",
                    MinTemperature = $"{w.Main.TempMin}°C",
                    MaxTemperature = $"{w.Main.TempMax}°C",
                    Icon = $"https://openweathermap.org/img/wn/{w.Weather.FirstOrDefault()?.Icon ?? ""}.png"
                    
                }).ToList();

                var currDayWeatherData = new CurrDayWeatherDataViewModel(
                    dayName: DateTime.Parse(group.Key).ToString("dddd"),
                    currentTemperature: $"{group.First().Main.Temperature.ToString()}°C",
                    minTemperature: group.Min(w => w.Main.TempMin).ToString(),
                    maxTemperature: group.Max(w => w.Main.TempMax).ToString(),
                    feelsLike: $"{group.First().Main.FeelsLike}°C",
                    humidity: $"{group.First().Main.Humidity}%",
                    hourlyForecasts: hourlyForecasts
                );

                CurrDayWeatherDataViewModels.Add(currDayWeatherData);
            }
        }

        public static CurrDayWeatherDataViewModel GetCurrDayWeather(int index)
            => CurrDayWeatherDataViewModels[index];
    }
}
