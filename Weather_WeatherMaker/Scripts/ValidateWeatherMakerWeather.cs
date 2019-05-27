using DigitalRuby.WeatherMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using wizardscode.utility;

namespace wizardscode.environment.weathermaker
{
    public class ValidateWeatherMakerWeather : ValidationTest<WeatherPluginManager>
    {
        const string PLUGIN_KEY = "Weather Plugin";
        const string WEATHER_MAKER_SCRIPT_KEY = PLUGIN_KEY + " (Weather Maker)";
        const string WEATHER_MAKER_CAMERA_KEY = PLUGIN_KEY + " Camera";
        const string WEATHER_MAKER_LIGHTING_KEY = PLUGIN_KEY + " Lighting";

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

        /**
         * FIXME: mover to new settings SO model
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

        // Check Camera Settings
        Camera camera = Camera.main;
        if (camera.clearFlags != CameraClearFlags.SolidColor || camera.backgroundColor != new Color(0, 0, 0, 0))
        {
            result = ValidationHelper.Validations.GetOrCreate(WEATHER_MAKER_CAMERA_KEY);
            result.Message = "Weather Plugin is enabled but the camera is not setup to show clouds or skysphere.";
            result.impact = ValidationResult.Level.Warning;
            result.resolutionCallback = SetupCamera;
            localCollection.AddOrUpdate(result);
        } else
        {
            ValidationHelper.Validations.Remove(WEATHER_MAKER_CAMERA_KEY);
        }

        // Check Lighting
        if (RenderSettings.defaultReflectionMode != UnityEngine.Rendering.DefaultReflectionMode.Custom)
        {
            result = ValidationHelper.Validations.GetOrCreate(WEATHER_MAKER_LIGHTING_KEY);
            result.Message = "Weather Plugin is enabled but the scene lighting is not setup correctly.";
            result.impact = ValidationResult.Level.Warning;
            result.resolutionCallback = SetupLighting;
            localCollection.AddOrUpdate(result);
        }
        else
        {
            ValidationHelper.Validations.Remove(WEATHER_MAKER_LIGHTING_KEY);
        }
        return localCollection;
    }

    private void SetupLighting()
    {
        RenderSettings.defaultReflectionMode = UnityEngine.Rendering.DefaultReflectionMode.Custom;
    }

    private void SetupCamera()
    {
        Camera camera = Camera.main;
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = new Color(0, 0, 0, 0);
    }

    private void AddWeatherMakerScript()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/WeatherMaker/Prefab/WeatherMakerPrefab.prefab");
        GameObject.Instantiate(prefab);
    }
*/
    }
}