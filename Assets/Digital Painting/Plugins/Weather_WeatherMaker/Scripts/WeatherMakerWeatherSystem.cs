using DigitalRuby.WeatherMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace wizardscode.environment.weather
{
    [CreateAssetMenu(fileName = "WeatherMakerWeatherSystem", menuName = "Wizards Code/Weather/Weather Maker")]
    public class WeatherMakerWeatherSystem : AbstractWeatherSystem
    {
        [Header("Digital Ruby's Weather Maker")]
        [Tooltip("The Weather Maker Script prefab to use to add the WeatherMaker scripts.")]
        public GameObject WeatherMakerScriptPrefab;

        [Header("Base Profiles for Weather Maker")]
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
        
        private GameObject weatherMaker;
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
            if (WeatherMakerScriptPrefab == null)
            {
                Debug.LogError("You have not defined a WeatherMakerScript in the WeatherMakerDayNightCycleConfig. There is a sample provided in the `Common/Prefabs` folder of this plugin.");
            }

            WeatherMakerScript component = GameObject.FindObjectOfType<WeatherMakerScript>();
            if (component == null)
            {
                weatherMaker = Instantiate(WeatherMakerScriptPrefab.gameObject);
                weatherMaker.name = "Weather Maker";
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

            //SetupCamera();

            // Since we've changed the config of the weather maker manager we need to trigger the OnEnable method so that it re-initializes
            weatherMaker.SetActive(false);
            weatherMaker.SetActive(true);

            WeatherMakerScript.Instance.RaiseWeatherProfileChanged(null, clearProfile, 1, 200, true, null);
        }

        private void SetupCamera()
        {
            Rigidbody rigidBody = Camera.main.gameObject.AddComponent<Rigidbody>();
            rigidBody.useGravity = false;
            rigidBody.isKinematic = true;

            SphereCollider collider = Camera.main.gameObject.AddComponent<SphereCollider>();
            collider.isTrigger = true;
            collider.radius = 0.001f;

            Camera.main.gameObject.AddComponent<WeatherMakerSoundZoneScript>();

            Camera.main.clearFlags = CameraClearFlags.Color;
            Camera.main.backgroundColor = new Color(0, 0, 0, 0);
            Camera.main.farClipPlane = 10000;

            weatherMaker.GetComponent<WeatherMakerScript>().AllowCameras[0] = Camera.main;
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