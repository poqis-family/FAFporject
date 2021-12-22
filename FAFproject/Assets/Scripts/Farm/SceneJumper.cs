using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneJumper
{
    public static SceneJumper _Instance;
    public SceneJumper(){
        _Instance = this;
    }

    public void SceneJump(string SceneName)
    {
        if (SceneManager.sceneCount <= 1)
        {
            AsyncOperation test = SceneManager.LoadSceneAsync(SceneName);
        }
        //////我写到这SceneManager.sceneLoaded
    }
}
