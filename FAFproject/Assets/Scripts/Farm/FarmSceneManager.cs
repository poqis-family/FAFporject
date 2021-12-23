using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public  class FarmSceneManager : MonoBehaviour
{
    private  AsyncOperation asyncOperation;
    public  SceneEnum.Scenes lastScene;
    public  SceneEnum.Scenes nowScene;
    public  bool startLoaded;
    public static FarmSceneManager _Instance;

    private void Awake()
    {
        _Instance = this;
    }

    public  void SceneJump(SceneEnum.Scenes sceneEnum)
    {
        FarmDataManager._Instance.SavePlotDataToScene();
        startLoaded = true;
        lastScene = nowScene;
        nowScene = sceneEnum;
        asyncOperation = SceneManager.LoadSceneAsync(sceneEnum.ToString());
    }
    public  void SceneJump(SceneEnum.Scenes sceneEnum,SceneEnum.Scenes fromScene)
    {
        nowScene = fromScene;
        SceneJump(sceneEnum);
    }

    private void Update()
    {
        if (startLoaded == true)
        {
            CheckLoadDone();
        }
    }

    public void CheckLoadDone()
    {
        if (asyncOperation.isDone)
        {
            FarmDataManager._Instance.LoadScenePlotData();
            RefreshMap();
            startLoaded = false;
        }
    }

    public void RefreshMap()
    {
        TileMapController._Instance.RefreshTilemap();
    }

}
