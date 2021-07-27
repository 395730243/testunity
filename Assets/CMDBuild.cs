using System;
using System.Collections.Generic;
 
using UnityEditor;
using UnityEditor.Build.Reporting;
 
using Debug = UnityEngine.Debug;
 
public class CMDBuild : Editor
{
    const string LOCATION_PATH_ANDROID = "./android.apk";
    public static void CMDBuildAndroid(){
 
       
        string ProductName = "";
        
 
 
        Debug.Log("------------- 接收命令行参数 -------------");
        List<string> commondList = new List<string>();
        foreach (string arg in System.Environment.GetCommandLineArgs()) {
            Debug.Log("命令行传递过来参数：" + arg);
            commondList.Add(arg);
        }
 
        try {
            Debug.Log("命令行传递过来参数数量：" + commondList.Count);
            ProductName = commondList[commondList.Count - 1];
           
        }
        catch (Exception e) {
            Debug.Log("接收命令行参数出错： "+e.Message);
        }
 
        //ProductName Jenkins穿來的參數 自行時使用
        ProjectBuildExecute(BuildTarget.Android);
       
    }
 
 
    public static void ProjectBuildExecute(BuildTarget target)
    {
 
        //Switch Platform
        SwitchPlatform(target);
 
 
   
        List<string> scenes = new List<string>();
        foreach (UnityEditor.EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                if (System.IO.File.Exists(scene.path))
                {
                    Debug.Log("Add Scene (" + scene.path + ")");
                    scenes.Add(scene.path);
                }
            }
        }
 
       
        BuildReport report = BuildPipeline.BuildPlayer(scenes.ToArray(), LOCATION_PATH_ANDROID, target, BuildOptions.None);
        if (report.summary.result != BuildResult.Succeeded)
        {
            Debug.LogError("打包失败。(" + report.summary.ToString() + ")");
        }
        
    }
    
    private static void SwitchPlatform(BuildTarget target)
    {
 
        if (EditorUserBuildSettings.activeBuildTarget != target)
        {
            if (target == BuildTarget.iOS)
            {
                EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.iOS, BuildTarget.iOS);
            }
            if (target == BuildTarget.Android)
            {
                EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
            }
        }
 
    }
 
 
 
}
