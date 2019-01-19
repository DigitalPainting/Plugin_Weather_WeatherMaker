using UnityEditor;
using UnityEngine;

public class WeatherMakerPackageBuilder : PackageBuilder
{
    [MenuItem("Digital Painting/Build/Build Weather Maker Plugin")]
    new public static void Build()
    {
        string rootDir = "Assets\\Digital Painting\\Plugins\\Weather_WeatherMaker";
        string packageName = "Weather_WeatherMaker.unitypackage";

        MoveExcludedFiles(rootDir);

        AssetDatabase.ExportPackage(rootDir, packageName, ExportPackageOptions.Interactive | ExportPackageOptions.Recurse);
        Debug.Log("Exported " + packageName);

        RecoverExcludedFiles(rootDir);
    }
}
