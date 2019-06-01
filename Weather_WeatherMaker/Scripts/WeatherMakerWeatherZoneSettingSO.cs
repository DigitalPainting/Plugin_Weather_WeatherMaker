using DigitalRuby.WeatherMaker;
using UnityEngine;

namespace wizardscode.validation
{
    [CreateAssetMenu(fileName = "WeatherMakerWeatherZoneSettingSO", menuName = "Wizards Code/Validation/Weather Maker/Weather Zone Setting")]
    public class WeatherMakerWeatherZoneSettingSO : GenericSettingSO<WeatherMakerWeatherZoneScript>
    {
        public override string TestName
        {
            get { return "Weather Zone"; }
        }

        public override string SettingName { get { return "Weather Maker Prefab"; } }

        protected override WeatherMakerWeatherZoneScript ActualValue
        {
            get;
            set;
        }
    }
}