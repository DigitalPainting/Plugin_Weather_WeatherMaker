using UnityEngine;

namespace WizardsCode.Validation
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