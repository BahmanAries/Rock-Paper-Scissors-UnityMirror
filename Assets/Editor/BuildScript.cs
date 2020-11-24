using System.Linq;
using System.IO;
using System.Collections.Generic;
using UnityEditor;


public class BuildScript
{
    private static string _buildLocation;

    private static string[] GetScenes()
    {
        return EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray();
    }

    private static void SetupVariables()
    {
        if (!File.Exists("./buildManifest.txt"))
        {
            //There's no buildManifest.txt file set some defaults.
            PlayerSettings.productName = "Product Name Here";
            PlayerSettings.companyName = "Luke Parker";
            PlayerSettings.forceSingleInstance = true;
            PlayerSettings.bundleVersion = "0.0.0.0";
            _buildLocation = "./Build/";
        }
        else
        {
            // buildManifest file exists load it up and setup the variables.
            using (var fs = new FileStream("./buildManifest.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(fs))
                {
                    var fileData = new Dictionary<string, string>();
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine().Split('=');
                        fileData.Add(line[0], line[1].Replace("\"", ""));
                    }

                    PlayerSettings.productName = fileData["ProductName"];
                    PlayerSettings.companyName = fileData["CompanyName"];
                    PlayerSettings.forceSingleInstance = true;
                    PlayerSettings.bundleVersion = fileData["Version"];
                    _buildLocation = fileData["BuildLocation"] + "/" + fileData["Version"] + "/" + fileData["ProductName"].Replace(" ", "_");
                }
            }
        }
    }

    public static void Windows()
    {
        SetupVariables();
        BuildPipeline.BuildPlayer(GetScenes(), _buildLocation + ".exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
    }

    public static void MacOS()
    {
        SetupVariables();
        BuildPipeline.BuildPlayer(GetScenes(), _buildLocation + ".app", BuildTarget.StandaloneOSX, BuildOptions.None);
    }
}
