using DigitalRuby.WeatherMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using wizardscode.utility;
using wizardscode.validation;

namespace wizardscode.environment.weathermaker
{
    public class ValidateWeatherMakerDayNight : ValidateSimpleDayNightProfile
    {
        public override ValidationTest<DayNightPluginManager> Instance => new ValidateWeatherMakerDayNight();

        internal override string ProfileType { get { return "WeatherMakerDayNightProfile"; } }

        private DayNightPluginManager m_dayNightManager;

        private DayNightPluginManager DayNightManager
        {
            get
            {
                if (m_dayNightManager == null)
                {
                    m_dayNightManager = GameObject.FindObjectOfType<DayNightPluginManager>(); ;
                }
                return m_dayNightManager;
            }
        }
    }
}
