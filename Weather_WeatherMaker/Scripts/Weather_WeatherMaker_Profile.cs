using DigitalRuby.WeatherMaker;
using UnityEngine;
using wizardscode.editor;
using wizardscode.validation;

namespace wizardscode.environment.weathermaker
{
    [CreateAssetMenu(fileName = "Weather_WeatherMaker_Profile", menuName = "Wizards Code/Plugin/Weather/Weather Maker")]
    public class Weather_WeatherMaker_Profile : AbstractWeatherProfile
    {
        [Header("Weather Maker")]
        [Tooltip("Weather Maker Prefab containing all the necessary components.")]
        [Expandable(isRequired: true, "Must provide a value for the Weather Maker prefab.")]
        public WeatherMakerPrefabSettingSO WeatherMakerPrefab;

        [Tooltip("Camera configured to display weather effects.")]
        [Expandable(isRequired: true, "Must provide a value for the Weather Maker camera prefab.")]
        public PrefabSettingSO cameraPrefab;

        [Header("Lighting")]
        [Tooltip("Reflection mode to ensure weather effects correctly affecting lighting.")]
        [Expandable(isRequired: true, "Must provide a value for the Weather Maker reflection mode setting.")]
        public ReflectionModeSettingSO ReflectionMode;

        [Tooltip("Color space for the Unity Player.")]
        [Expandable(isRequired: true, "Must provide a value for the color space setting.")]
        public ColorSpaceSettingSO ColorSpace;

        [Header("Graphics")]
        [Tooltip("Shadow rendering distance.")]
        [Expandable(isRequired: true, "Must provide a value for the shadow distance setting.")]
        public FloatSettingSO ShadowDistance;

        [Tooltip("Color space for the Unity Player.")]
        [Expandable(isRequired: true)]
        public ScreenSpaceShadowsSettingSO ScreenSpaceShadows;

        [Header("Base Profiles")]
        [Tooltip("Automated weather profile. If this is null then either manual or manager controlled weather is used. If this has a profile then it will override all other settings.")]
        public WeatherMakerProfileGroupScript automatedGroupProfile;
        [Tooltip("Profile for a clear weather.")]
        public WeatherMakerProfileScript clearProfile;
        [Tooltip("Profile for rainy weather.")]
        public WeatherMakerProfileScript rainProfile;
        [Tooltip("Profile for snowy weather")]
        public WeatherMakerProfileScript snowProfile;
        [Tooltip("Profile for sleet.")]
        public WeatherMakerProfileScript sleetProfile;
        [Tooltip("Profile for hail.")]
        public WeatherMakerProfileScript hailProfile;

        private WeatherPluginManager manager;
        private GameObject weatherMaker;
        private WeatherMakerWeatherZoneScript zone;

        private GameObject weather;
        private float timeToNextUpdate;
        private DayNightPluginManager dayNightManager;

        public WeatherMakerProfileScript CurrentWeatherMakerProfile
        {
            get; set;
        }

        internal override void Initialize()
        {
            RenderSettings.fog = false;
        }

        internal override void Start()
        {
            manager = FindObjectOfType<WeatherPluginManager>();
            if (manager == null)
            {
                Debug.LogError("Cannot find Weather Manager.");
            }

            zone = FindObjectOfType<WeatherMakerWeatherZoneScript>();
            if (zone == null)
            {
                Debug.LogError("Unable to find a WeatherMakerWeatherZoneScript");
            }


            WeatherMakerScript component = GameObject.FindObjectOfType<WeatherMakerScript>();
            if (component == null)
            {
                Debug.LogError("You don't have a WeatherMakerScript in your scene. Please see the Weather Maker Weather System plugin README for instructions.");
                return;
            }
            else
            {
                weatherMaker = component.gameObject;
            }

            if (weatherMaker == null)
            {
                Debug.LogError("Unable to find or instantiate an object with the WeatherMakerScript attached");
            }

            DontDestroyOnLoad(weatherMaker);

            if (automatedGroupProfile != null)
            {
                zone.ProfileGroup = automatedGroupProfile;
                zone.SingleProfile = null;
                manager.isAuto = false;

                float transisitonDuration = 15f;
                float holdDuration = 20f;

                dayNightManager = FindObjectOfType<DayNightPluginManager>();
                if (dayNightManager != null)
                {
                    transisitonDuration = dayNightManager.GameSecondsToRealSeconds(120 * 60); // 2 hour game time
                    holdDuration = dayNightManager.GameSecondsToRealSeconds(15 * 60); // 1/4 hour game time
                }

                automatedGroupProfile.TransitionDuration.Minimum = transisitonDuration * 0.75f;
                automatedGroupProfile.TransitionDuration.Maximum = transisitonDuration * 1.25f;
                automatedGroupProfile.HoldDuration.Minimum = holdDuration * 0.75f;
                automatedGroupProfile.HoldDuration.Maximum = holdDuration * 1.25f;
            }

            // Since we've changed the config of the weather maker manager we need to trigger the OnEnable method so that it re-initializes
            weatherMaker.SetActive(false);
            weatherMaker.SetActive(true);

            WeatherMakerScript.Instance.RaiseWeatherProfileChanged(null, clearProfile, 1, 20, true, null);
        }

        internal override void Update()
        {
            if (CurrentProfile.isDirty)
            {
                switch (CurrentProfile.PrecipitationType)
                {
                    case WeatherProfile.PrecipitationTypeEnum.Clear:
                        ChangeWeather(clearProfile);
                        break;
                    case WeatherProfile.PrecipitationTypeEnum.Rain:
                        rainProfile.PrecipitationProfile.IntensityRange = new RangeOfFloats(CurrentProfile.PrecipitationIntensity * 0.8f, CurrentProfile.PrecipitationIntensity);
                        ChangeWeather(rainProfile);
                        break;
                    case WeatherProfile.PrecipitationTypeEnum.Hail:
                        rainProfile.PrecipitationProfile.IntensityRange = new RangeOfFloats(CurrentProfile.PrecipitationIntensity * 0.8f, CurrentProfile.PrecipitationIntensity);
                        ChangeWeather(hailProfile);
                        break;
                    case WeatherProfile.PrecipitationTypeEnum.Sleet:
                        rainProfile.PrecipitationProfile.IntensityRange = new RangeOfFloats(CurrentProfile.PrecipitationIntensity * 0.8f, CurrentProfile.PrecipitationIntensity);
                        ChangeWeather(sleetProfile);
                        break;
                    case WeatherProfile.PrecipitationTypeEnum.Snow:
                        rainProfile.PrecipitationProfile.IntensityRange = new RangeOfFloats(CurrentProfile.PrecipitationIntensity * 0.8f, CurrentProfile.PrecipitationIntensity);
                        ChangeWeather(snowProfile);
                        break;
                    default:
                        Debug.LogError("WeatherMakerWeatherSystem does not know how to handle precipitation type '" + CurrentProfile.PrecipitationType + "' using clear profile");
                        ChangeWeather(clearProfile);
                        break;
                }

                CurrentProfile.isDirty = false;
            }
        }

        private void ChangeWeather(WeatherMakerProfileScript newProfile)
        {
            if (WeatherMakerScript.Instance == null)
            {
                Debug.LogWarning("WeatherMakerScript instance not initialized correctly");
            }
            else
            {
                WeatherMakerProfileScript lastProfile = CurrentWeatherMakerProfile;
                CurrentWeatherMakerProfile = newProfile;
                WeatherMakerScript.Instance.RaiseWeatherProfileChanged(lastProfile, newProfile, 20, UnityEngine.Random.value * 20 + 10, true, null);
            }
        }
    }
}