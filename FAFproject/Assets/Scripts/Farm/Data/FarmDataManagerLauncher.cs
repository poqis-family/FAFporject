using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FarmDataManagerLauncher : MonoBehaviour
{

    void Awake()
    {
        FarmDataManager farmDataManager = new FarmDataManager();
    }
    public void Click()
    {
        AsyncOperation test = SceneManager.LoadSceneAsync("Farm");
    }
}
