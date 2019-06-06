using DigitalRuby.WeatherMaker;
using UnityEngine;

namespace wizardscode.validation
{
    [CreateAssetMenu(fileName = "WeatherMakerWeatherZoneSettingSO", menuName = "Wizards Code/Validation/Weather Maker/Weather Zone Setting")]
    public class WeatherMakerWeatherZoneSettingSO : GenericSettingSO<WeatherMakerWeatherZoneScript>
    {
        protected override WeatherMakerWeatherZoneScript ActualValue
        {
            get;
            set;
        }
    }
}