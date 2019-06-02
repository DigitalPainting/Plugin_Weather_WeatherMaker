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

        protected override WeatherMakerScript ActualValue
        {
            get {
                GameObject instance = GetFirstInstanceInScene();
                if (instance)
                {
                    return instance.GetComponent<WeatherMakerScript>();
                }
                else
                {
                    return null;
                }
            }
            set => throw new NotImplementedException();
        }
    }
}