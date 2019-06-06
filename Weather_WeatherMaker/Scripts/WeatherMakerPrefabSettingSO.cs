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
    public class WeatherMakerPrefabSettingSO : PrefabSettingSO
    {
        /*
        protected override UnityEngine.Object ActualValue
        {
            get {
                if (Instance)
                {
                    return ((GameObject)Instance).GetComponent<WeatherMakerScript>();
                }
                else
                {
                    return null;
                }
            }
            set => throw new NotImplementedException();
        }
        */
    }
}