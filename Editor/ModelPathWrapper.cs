using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class ModelPathWrapper : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }

    public void OnPreprocessBuild(BuildReport report)
    {
        string[] fileEntries = Directory.GetFiles("Assets/Resources/GenshinModels", "*.fbx", SearchOption.AllDirectories);
        using (StreamWriter sw = new StreamWriter("Assets/Resources/GenshinModels/FigurePath.txt", false))
        {

            foreach (string filename in fileEntries)
            {
                sw.WriteLine(filename.Substring(17, filename.Length - 21));
            }

        }
    }
}
