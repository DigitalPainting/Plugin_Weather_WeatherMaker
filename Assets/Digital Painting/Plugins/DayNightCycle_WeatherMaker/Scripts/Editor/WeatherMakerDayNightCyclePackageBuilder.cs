using UnityEditor;
using UnityEngine;

public class WeatherMakerDayNightCyclePackageBuilder : PackageBuilder
{
    [MenuItem("Digital Painting/Build/Build Day Night Cycle (Weather Maker) Plugin")]
    new public static void Build()
    {
        string[] rootDirs = { "Assets\\Digital Painting\\Plugins\\DayNightCycle_WeatherMaker", "Assets\\Digital Painting\\Plugins\\WeatherMakerCommon" };
        string packageName = @"..\DayNightCycle_WeatherMaker.unitypackage";

        foreach (string rootDir in rootDirs)
        {
            MoveExcludedFiles(rootDir);
        }

        AssetDatabase.ExportPackage(rootDirs, packageName, ExportPackageOptions.Interactive | ExportPackageOptions.Recurse );
        Debug.Log("Exported " + packageName);


        foreach (string rootDir in rootDirs)
        {
            RecoverExcludedFiles(rootDir);
        }
    }

}
