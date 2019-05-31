using DigitalRuby.WeatherMaker;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using wizardscode.environment;

namespace wizardscode.validation
{
    [CreateAssetMenu(fileName = "WeatherMakerPrefabSettingSO", menuName = "Wizards Code/Validation/Weather Maker/Prefab Setting")]
    public class WeatherMakerPrefabSettingSO : GenericSettingSO<WeatherMakerScript>
    {
        public override string TestName
        {
            get { return "Instance"; }
        }

        public override string SettingName { get { return "Weather Maker Prefab"; } }

        protected override WeatherMakerScript ActualValue
        {
            get { return GetFirstInstanceInScene().GetComponent<WeatherMakerScript>(); }
            set => throw new NotImplementedException();
        }
    }
}