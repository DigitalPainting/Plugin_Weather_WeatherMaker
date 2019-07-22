#if WEATHER_MAKER_PRESENT
using DigitalRuby.WeatherMaker;
#endif
using UnityEngine;
using WizardsCode.Editor;
using WizardsCode.Validation;

namespace WizardsCode.Environment.WeatherMaker
{
    [CreateAssetMenu(fileName = "WeatherMakerDayNightCycleConfig", menuName = "Wizards Code/Day Night Cycle/Weather Maker Day Night Cycle Config")]
    public class WeatherMakerDayNightProfile : AbstractDayNightProfile
    {
        [Header("Weather Maker")]
        [Tooltip("The Weather Maker prefab to add to the scene.")]
        [Expandable(isRequired: true)]
        public PrefabSettingSO weatherMakerPrefab;

#if WEATHER_MAKER_PRESENT
        [Tooltip("The Weather Maker Profile to use")]
        [Expandable(isRequired: true, isRequiredMessage: "Select or create a weather maker profile.")]
        public WeatherMakerDayNightCycleProfileScript weatherMakerProfile;
#endif

        [Tooltip("Camera that allows the moon and starts to shine through.")]
        [Expandable(isRequired: true)]
        public PrefabSettingSO cameraPrefab;

        [Header("Lighting")]
        [Tooltip("Camera that allows the moon and starts to shine through.")]
        [Expandable(isRequired: true)]
        public ReflectionModeSettingSO reflectionMode;

        private GameObject weatherMakerScript;
        private GameObject dayNight;
        private float daySpeed;
        private float nightSpeed;

        internal override void Initialize()
        {

#if WEATHER_MAKER_PRESENT
            WeatherMakerScript component = GameObject.FindObjectOfType<WeatherMakerScript>();
            if ( component == null)
            {
                Debug.LogError("You don't have a WeatherMakerScript in your scene. Please see the Weather Maker Day Night Cycle plugin README for instructions.");
                return;
            } else
            {
                weatherMakerScript = component.gameObject;
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
#endif
            base.Initialize();
        }

        internal override void InitializeCamera()
        {
        }

        internal override void InitializeLighting()
        {
            RenderSettings.fog = false;
        }

        internal override void InitializeSun()
        {
        }

        internal override void InitializeTiming()
        {
#if WEATHER_MAKER_PRESENT
            WeatherMakerDayNightCycleManagerScript.Instance.TimeOfDay = startTime;
            daySpeed = 1440 / dayCycleInMinutes;
            WeatherMakerDayNightCycleManagerScript.Instance.Speed = daySpeed;
            nightSpeed = daySpeed; // don't currently support separate day and night speeds
            WeatherMakerDayNightCycleManagerScript.Instance.NightSpeed = nightSpeed;

            WeatherMakerDayNightCycleManagerScript.Instance.DayNightProfile = weatherMakerProfile;
            WeatherMakerDayNightCycleManagerScript.Instance.DayNightProfile.UpdateFromProfile(true);
#endif
        }

        internal override void SetTime(float timeInSeconds)
        {
#if WEATHER_MAKER_PRESENT
            WeatherMakerDayNightCycleManagerScript.Instance.TimeOfDay = timeInSeconds;
#endif
        }


        internal override float GetTime()
        {
#if WEATHER_MAKER_PRESENT
            return WeatherMakerDayNightCycleManagerScript.Instance.TimeOfDay;
#else
            return 0;
#endif
        }

        internal override void Update()
        {
            daySpeed = 1440 / dayCycleInMinutes;
            nightSpeed = daySpeed; // don't currently support separate day and night speeds

#if WEATHER_MAKER_PRESENT
            if (daySpeed != 1440 / dayCycleInMinutes)
            {
                weatherMakerProfile.Speed = daySpeed;
                WeatherMakerDayNightCycleManagerScript.Instance.Speed = daySpeed;
            }

            if (nightSpeed != 1440 / dayCycleInMinutes)
            {
                weatherMakerProfile.NightSpeed = nightSpeed;
                WeatherMakerDayNightCycleManagerScript.Instance.NightSpeed = nightSpeed;
            }
#endif
        }
    }
}
