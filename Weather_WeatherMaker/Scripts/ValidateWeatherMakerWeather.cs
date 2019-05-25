using DigitalRuby.WeatherMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using wizardscode.utility;

namespace wizardscode.environment.weathermaker
{
    public class ValidateWeatherMakerWeather : IValidationTest
    {
        const string PLUGIN_KEY = "Weather Plugin";
        const string WEATHER_MAKER_SCRIPT_KEY = PLUGIN_KEY + " (Weather Maker)";

        private WeatherPluginManager m_weatherManager;

        public IValidationTest Instance => new ValidateWeatherMakerWeather();

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

        public ValidationResultCollection Execute()
        {
            ValidationResultCollection localCollection = new ValidationResultCollection();
            ValidationResult result;
            
            if (WeatherManager == null)
            {
                return localCollection;
            }
            
            WeatherMakerScript wmScript = GameObject.FindObjectOfType<WeatherMakerScript>();
            if (WeatherManager && WeatherManager.Profile is WeatherMakerWeatherProfile)
            {
                if (wmScript == null)
                {
                    result = ValidationHelper.Validations.GetOrCreate(WEATHER_MAKER_SCRIPT_KEY);
                    result.Message = "Weather Plugin is enabled and configured to use Weather Maker, but the `WeatherMakerScript` is not present in the scene.";
                    result.impact = ValidationResult.Level.Error;
                    result.resolutionCallback = AddWeatherMakerScript;
                    localCollection.AddOrUpdate(result);
                    return localCollection;
                }
                else
                {
                    ValidationHelper.Validations.Remove(WEATHER_MAKER_SCRIPT_KEY);
                }
            }

            return localCollection;
        }

        private void AddWeatherMakerScript()
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/WeatherMaker/Prefab/WeatherMakerPrefab.prefab");
            GameObject.Instantiate(prefab);
        }
    }
}