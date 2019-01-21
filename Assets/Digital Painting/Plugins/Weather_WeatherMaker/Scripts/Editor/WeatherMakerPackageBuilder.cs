using UnityEditor;
using UnityEngine;

public class WeatherMakerPackageBuilder : PackageBuilder
{
    [MenuItem("Digital Painting/Build/Build Weather Maker Plugin")]
    new public static void Build()
    {
        string[] rootDirs = { "Assets\\Digital Painting\\Plugins\\Weather_WeatherMaker", "Assets\\Digital Painting\\Plugins\\WeatherMakerCommon" };
        string packageName = "Weather_WeatherMaker.unitypackage";
        
        foreach (string rootDir in rootDirs)
        {
            MoveExcludedFiles(rootDir);
        }

        AssetDatabase.ExportPackage(rootDirs, packageName, ExportPackageOptions.Interactive | ExportPackageOptions.Recurse);
        Debug.Log("Exported " + packageName);


        foreach (string rootDir in rootDirs)
        {
            RecoverExcludedFiles(rootDir);
        }
    }
}
