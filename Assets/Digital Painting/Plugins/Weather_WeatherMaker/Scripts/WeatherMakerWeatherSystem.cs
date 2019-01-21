using DigitalRuby.WeatherMaker;
using UnityEngine;

namespace wizardscode.environment.weather
{
    [CreateAssetMenu(fileName = "WeatherMakerWeatherSystem", menuName = "Wizards Code/Weather/Weather Maker")]
    public class WeatherMakerWeatherSystem : AbstractWeatherSystem
    {
        [Header("Base Profiles for Weather Maker")]
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

        private WeatherManager manager;
        private GameObject weatherMaker;
        private WeatherMakerWeatherZoneScript zone;

        private GameObject weather;
        private float timeToNextUpdate;

        public WeatherMakerProfileScript CurrentWeatherMakerProfile
        {
            get; set;
        }

        internal override void Initialize()
        {
            RenderSettings.fog = false;
            RenderSettings.skybox = skyboxMaterial;


        }

        internal override void Start()
        {
            manager = FindObjectOfType<WeatherManager>();
            if (manager == null)
            {
                Debug.LogError("Cannot find Weather Manager.");
            }

            zone = FindObjectOfType<WeatherMakerWeatherZoneScript>();
            if (zone == null)
            {
                Debug.LogError("Unable to fine a WeatherMakerWeatherZoneScript");
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