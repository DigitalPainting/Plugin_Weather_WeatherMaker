using DigitalRuby.WeatherMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using wizardscode.utility;
using wizardscode.validation;

namespace wizardscode.environment.weathermaker
{
    public class ValidateWeatherMakerWeather : ValidationTest<WeatherPluginManager>
    {

        public override ValidationTest<WeatherPluginManager> Instance => new ValidateWeatherMakerWeather();

        internal override string ProfileType { get { return "WeatherMakerWeatherProfile"; } }
        
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