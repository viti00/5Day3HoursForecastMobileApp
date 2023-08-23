using System;
using System.Collections.Generic;
using System.Text;
using UnweWeatherApp.ViewModels;

namespace UnweWeatherApp
{
    public static class GlobalVariables
    {
        public static List<CurrDayWeatherDataViewModel> CurrDayWeatherDataViewModels = new List<CurrDayWeatherDataViewModel>();
        public static City cityVariable = new City();
    }
}
