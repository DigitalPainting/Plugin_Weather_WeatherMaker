using System;
using WizardsCode.Validation;

namespace WizardsCode.Environment.WeatherMaker
{
    public class ValidateWeatherMakerDayNight : ValidateSimpleDayNightProfile
    {
        public override ValidationTest<DayNightPluginManager> Instance => new ValidateWeatherMakerDayNight();

        internal override Type ProfileType => typeof(WeatherMakerDayNightProfile);

    }
}
