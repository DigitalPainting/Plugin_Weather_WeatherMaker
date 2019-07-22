#if WEATHER_MAKER_PRESENT
using DigitalRuby.WeatherMaker;
#endif
using System;
using System.Collections.Generic;
using UnityEngine;
using WizardsCode.Plugin;

namespace WizardsCode.Validation
{
    [CreateAssetMenu(fileName = "WeatherMakerSunSettingSO", menuName = "Wizards Code/Validation/Weather Maker/Sun Prefab")]
    public class WeatherMakerSunSettingsSO : SunSettingSO
    {

        public override void Fix()
        {
            RenderSettings.sun = GetFirstInstanceInScene().GetComponent<Light>();
        }

        internal override ValidationResult ValidateSetting(Type validationTest, AbstractPluginManager pluginManager)
        {
            ValidationResult result = base.ValidateSetting(validationTest, pluginManager);

            if (result.impact != ValidationResult.Level.OK)
            {
                return result;
            }

            GameObject sun = GetFirstInstanceInScene();
#if WEATHER_MAKER_PRESENT
            if (sun.GetComponent<WeatherMakerCelestialObjectScript>() == null)
            {
                ResolutionCallback callback = new ResolutionCallback(ConfigureSunAsCelestialObject);
                result = GetErrorResult(TestName, pluginManager, "The sun does not have the celestial object component attached.", validationTest.Name);
                List<ResolutionCallback> callbacks = new List<ResolutionCallback>();
                callbacks.Add(callback);
                result.Callbacks = callbacks;
                return result; 
            }
#endif

            return GetPassResult(TestName, pluginManager, validationTest.Name);
        }

        private void ConfigureSunAsCelestialObject()
        {
            GameObject sun = GetFirstInstanceInScene();
#if WEATHER_MAKER_PRESENT
            WeatherMakerCelestialObjectScript celestial = sun.AddComponent<WeatherMakerCelestialObjectScript>();
            celestial.IsSun = true;
#endif
        }
    }
}