using System;
using UnityEngine;
using WizardsCode.Validation;

namespace WizardsCode.Environment.WeatherMaker
{
    public class ValidateWeatherMakerWeather : ValidationTest<WeatherPluginManager>
    {

        public override ValidationTest<WeatherPluginManager> Instance => new ValidateWeatherMakerWeather();

        internal override Type ProfileType => typeof(Weather_WeatherMaker_Profile);
        
        private WeatherPluginManager m_weatherManager;

        private WeatherPluginManager WeatherManager
        {
            get
            {
                if (m_weatherManager == null)
                {
                    m_weatherManager = GameObject.FindObjectOfType<WeatherPluginManager>(); ;
                }
                return m_weatherManager;
            }
        }
    }
}