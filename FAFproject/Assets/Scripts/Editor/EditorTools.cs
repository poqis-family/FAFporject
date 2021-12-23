using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using System.IO;

public class EditorTools
{
    
    [MenuItem("Edit/Run _F5")]
    public static void OpenLaunchScene()
    {
        Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        if (scene.name == "FarmStart")
        {
            EditorApplication.isPlaying = true;
            //EditorApplication.ExecuteMenuItem("Edit/Play");
        }
        else
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene("Assets/Scenes/FarmStart.unity");
            }
        }
    }
    
}