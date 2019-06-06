using DigitalRuby.WeatherMaker;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace wizardscode.validation
{
    [CreateAssetMenu(fileName = "WeatherMakerSunSettingSO", menuName = "Wizards Code/Validation/Weather Maker/Sun Prefab")]
    public class WeatherMakerSunSettingsSO : SunSettingSO
    {

        public override void Fix()
        {
            RenderSettings.sun = GetFirstInstanceInScene().GetComponent<Light>();
        }

        internal override ValidationResult ValidateSetting(Type validationTest)
        {
            ValidationResult result = base.ValidateSetting(validationTest);

            if (result.impact != ValidationResult.Level.OK)
            {
                return result;
            }

            GameObject sun = GetFirstInstanceInScene();
            if (sun.GetComponent<WeatherMakerCelestialObjectScript>() == null)
            {
                ResolutionCallback callback = new ResolutionCallback(ConfigureSunAsCelestialObject);
                result = GetErrorResult(TestName, "The sun does not have the celestial object component attached.", validationTest.Name);
                List<ResolutionCallback> callbacks = new List<ResolutionCallback>();
                callbacks.Add(callback);
                result.Callbacks = callbacks;
                return result; 
            }

            return GetPassResult(TestName, validationTest.Name);
        }

        private void ConfigureSunAsCelestialObject()
        {
            GameObject sun = GetFirstInstanceInScene();
            WeatherMakerCelestialObjectScript celestial = sun.AddComponent<WeatherMakerCelestialObjectScript>();
            celestial.IsSun = true;
        }
    }
}