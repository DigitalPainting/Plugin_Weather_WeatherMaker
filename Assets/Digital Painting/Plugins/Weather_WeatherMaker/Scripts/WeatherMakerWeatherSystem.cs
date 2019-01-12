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
        public WeatherMakerScript WeatherMakerScriptPrefab;
        [Tooltip("Interval between weather updates in seconds.")]
        public float UpdateInterval = 2;

        [Header("Manager Prefabs")]
        public WeatherMakerAudioManagerScript AudioManagerPrefab;
        public WeatherMakerCloudManagerScript FullScreenEffectsPrefab;
        public WeatherMakerWeatherZoneScript GlobalWeatherZonePrefab;
        public WeatherMakerPrecipitationManagerScript PrecipitationManagerPrefab;
        public WeatherMakerSkySphereScript SkySpherePrefab;
        public WeatherMakerWindManagerScript WindManagerPrefab;

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

        WeatherMakerWeatherZoneScript zone;

        private GameObject weatherMaker;
        private GameObject weather;
        private float timeToNextUpdate;

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

            // Instantiate all the Weather controller prefabs
            GameObject go = Instantiate(AudioManagerPrefab.gameObject);
            go.name = "AudioManager";
            go.transform.parent = weatherMaker.transform;
            go = Instantiate(FullScreenEffectsPrefab.gameObject);
            go.name = "FullScreenEffects";
            go.transform.parent = weatherMaker.transform;
            go = Instantiate(GlobalWeatherZonePrefab.gameObject);
            go.name = "GlobalWeatherZone";
            go.transform.parent = weatherMaker.transform;
            zone = go.GetComponent<WeatherMakerWeatherZoneScript>();
            go = Instantiate(PrecipitationManagerPrefab.gameObject);
            go.name = "Precipitation";
            go.transform.parent = weatherMaker.transform;
            go = Instantiate(SkySpherePrefab.gameObject);
            go.name = "SkySphere";
            go.transform.parent = weatherMaker.transform;
            go = Instantiate(WindManagerPrefab.gameObject);
            go.name = "Wind";
            go.transform.parent = weatherMaker.transform;

            SetupCamera();

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
            timeToNextUpdate -= Time.deltaTime;

            if (timeToNextUpdate > 0)
            {
                return;
            }
            timeToNextUpdate = UpdateInterval;

            float change;
            if (Random.value > 0.5)
            {
                change = Random.value * 25;
            }
            else
            {
                change = -Random.value * 25;
            }

            if (precipitationIntensity == 0)
            {
                if (zone.SingleProfile != clearProfile)
                {
                    ChangeWeather(clearProfile);
                }
            } else
            {
                if (zone.SingleProfile != rainProfile)
                {
                    ChangeWeather(rainProfile);
                }
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
                WeatherMakerProfileScript lastProfile = zone.SingleProfile;
                WeatherMakerScript.Instance.RaiseWeatherProfileChanged(lastProfile, newProfile, 20, UnityEngine.Random.value * 20 + 10, true, null);
            }
        }
    }
}