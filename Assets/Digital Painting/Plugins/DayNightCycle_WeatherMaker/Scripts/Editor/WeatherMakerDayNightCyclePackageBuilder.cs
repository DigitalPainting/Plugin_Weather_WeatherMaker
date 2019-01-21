using UnityEditor;
using UnityEngine;

public class WeatherMakerDayNightCyclePackageBuilder : PackageBuilder
{
    [MenuItem("Digital Painting/Build/Build Day Night Cycle (Weather Maker) Plugin")]
    new public static void Build()
    {
        string rootDir = "Assets\\Digital Painting\\Plugins\\DayNightCycle_WeatherMaker";
        string packageName = "DayNightCycle_WeatherMaker.unitypackage";

        MoveExcludedFiles(rootDir);

        AssetDatabase.ExportPackage(rootDir, packageName, ExportPackageOptions.Interactive | ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies );
        Debug.Log("Exported " + packageName);

        RecoverExcludedFiles(rootDir);
    }

}
