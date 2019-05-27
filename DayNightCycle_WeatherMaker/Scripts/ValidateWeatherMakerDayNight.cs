using DigitalRuby.WeatherMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using wizardscode.utility;

namespace wizardscode.environment.weathermaker
{
    public class ValidateWeatherMakerDayNight : ValidationTest<DayNightPluginManager>
    {
        const string PLUGIN_KEY = "Day Night Plugin";
        const string WEATHER_MAKER_SCRIPT_KEY = PLUGIN_KEY + " (Weather Maker)";
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


        /**
         * FIXME: move to new settingsSO model
    public ValidationResultCollection Execute()
    {
        ValidationResultCollection localCollection = new ValidationResultCollection();
        ValidationResult result;

        if (DayNightManager == null)
        {
            return localCollection;
        }

        WeatherMakerScript wmScript = GameObject.FindObjectOfType<WeatherMakerScript>();

        if (DayNightManager && !(DayNightManager.Profile is WeatherMakerDayNightProfile))
        {
            return localCollection;
        }

        if (wmScript == null)
        {
            result = ValidationHelper.Validations.GetOrCreate(WEATHER_MAKER_SCRIPT_KEY);
            result.Message = "Day Night Plugin is enabled and configured to use Weather Maker, but the `WeatherMakerScript` is not present in the scene.";
            result.impact = ValidationResult.Level.Error;
            result.resolutionCallback = AddWeatherMakerScript;
            localCollection.AddOrUpdate(result);
            return localCollection;
        } else
        {
            ValidationHelper.Validations.Remove(WEATHER_MAKER_SCRIPT_KEY);
        }

        WeatherMakerDayNightProfile profile = (WeatherMakerDayNightProfile)DayNightManager.Profile;
        if (profile.weatherMakerProfile == null)
        {
            result = ValidationHelper.Validations.GetOrCreate(WEATHER_MAKER_SCRIPT_KEY);
            result.Message = "There is no WeatherMakerDayNightProfile specified.";
            result.impact = ValidationResult.Level.Error;
            result.resolutionCallback = SelectDayNightPluginManager;
            localCollection.AddOrUpdate(result);
            return localCollection;
        } else
        {
            ValidationHelper.Validations.Remove(WEATHER_MAKER_SCRIPT_KEY);
        }
        return localCollection;
    }

    private void SelectDayNightPluginManager()
    {
        Selection.activeGameObject = DayNightManager.gameObject;
    }

    private void AddWeatherMakerScript()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/WeatherMaker/Prefab/WeatherMakerPrefab.prefab");
        GameObject.Instantiate(prefab);
    }

        */
    }
}
