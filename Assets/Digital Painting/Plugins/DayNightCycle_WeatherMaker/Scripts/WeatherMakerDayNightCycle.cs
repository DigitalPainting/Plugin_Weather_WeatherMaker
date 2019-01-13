using DigitalRuby.WeatherMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using wizardscode.environment;

namespace wizardscode.environment.WeatherMaker
{
    [CreateAssetMenu(fileName = "WeatherMakerDayNightCycleConfig", menuName = "Wizards Code/Day Night Cycle/Weather Maker Day Night Cycle Config")]
    public class WeatherMakerDayNightCycle : AbstractDayNightCycle
    {
        [Header("Weather Maker Day Night")]
        [Tooltip("The Weather Maker Script prefab to use to add the WeatherMaker scripts.")]
        public GameObject WeatherMakerScriptPrefab;
        [Tooltip("The Weather Maker Profile to use")]
        public WeatherMakerDayNightCycleProfileScript weatherMakerProfile;

        private GameObject weatherMakerScript;
        private GameObject dayNight;
        
        internal override float GetTime()
        {
            return WeatherMakerDayNightCycleManagerScript.Instance.TimeOfDay;
        }

        internal override void Initialize(float startTime)
        {
            if (WeatherMakerScriptPrefab == null)
            {
                Debug.LogError("You have not defined a WeatherMakerScript in the WeatherMakerDayNightCycleConfig. There is a sample provided in the `Common/Prefabs` folder of this plugin.");
            }

            WeatherMakerScript component = GameObject.FindObjectOfType<WeatherMakerScript>();
            if ( component == null)
            {
                weatherMakerScript = Instantiate(WeatherMakerScriptPrefab.gameObject);
                weatherMakerScript.name = "Weather Maker";
            } else
            {
                weatherMakerScript = component.gameObject;
                // trigger OnEnable
                weatherMakerScript.SetActive(false);
                weatherMakerScript.SetActive(true);
            }

            WeatherMakerDayNightCycleManagerScript dayNightScript = FindObjectOfType<WeatherMakerDayNightCycleManagerScript>();
            if (dayNightScript == null)
            {
                Debug.LogError("Cannot find an object with the `WeatherMakerDayNightCycle` attached.");
            } else
            {
                dayNight = dayNightScript.gameObject;
            }            

            WeatherMakerDayNightCycleManagerScript.Instance.DayNightProfile = weatherMakerProfile;

            base.Initialize(startTime);
        }

        internal override void InitializeCamera()
        {
            Camera.main.farClipPlane = 2500;
            Camera.main.renderingPath = RenderingPath.DeferredLighting;
            Camera.main.allowHDR = true;
            Camera.main.allowMSAA = false;
        }

        internal override void InitializeLighting()
        {
            RenderSettings.sun = Sun;
            RenderSettings.fog = false;
        }

        internal override void InitializeSun()
        {
            GameObject go = GameObject.Find("Sun");
            if (go == null)
            {
                go = Instantiate(sunPrefab.gameObject);
                go.name = "Sun";
            }

            Sun = go.GetComponent<Light>();

            if (Sun == null)
            {
                Debug.LogError("Cannot find the sun, you need to set a prefab in the WeatherMakerDayNightCycleConfig. There is a suitable prefab in the prefabs folder of the WeatherMakerDayNightCycle plugin,");
            }
        }

        internal override void InitializeTiming()
        {
            WeatherMakerDayNightCycleManagerScript.Instance.TimeOfDay = startTime;
            float daySpeed = 1440 / dayCycleInMinutes;
            float nightSpeed = daySpeed;

            weatherMakerProfile.Speed = daySpeed;
            weatherMakerProfile.NightSpeed = daySpeed;

            WeatherMakerDayNightCycleManagerScript.Instance.DayNightProfile = weatherMakerProfile;
            WeatherMakerDayNightCycleManagerScript.Instance.DayNightProfile.UpdateFromProfile(true);
        }

        internal override void SetTime(float timeInSeconds)
        {
            WeatherMakerDayNightCycleManagerScript.Instance.TimeOfDay = timeInSeconds;
        }

        internal override void Update()
        {
        }
    }
}
