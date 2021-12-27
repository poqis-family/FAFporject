using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using WJExcelDataClass;

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
            RefreshPlayerPos();
            RefreshMap();
            startLoaded = false;
        }
    }

    public void RefreshMap()
    {
        TileMapController._Instance.RefreshTilemap();
    }

    public void RefreshPlayerPos()
    {
        Dictionary<int, ScenesJumpItem>.Enumerator itor =
            FarmDataManager._Instance.dataManager.p_ScenesJump.Dict.GetEnumerator();
        while (itor.MoveNext())
        {
            if (itor.Current.Value.fromScene == (int) FarmSceneManager._Instance.lastScene &&
                itor.Current.Value.targetScene == (int) FarmSceneManager._Instance.nowScene)
            {
                Vector3 vector3 = new Vector3(itor.Current.Value.playerPos[0], itor.Current.Value.playerPos[1], itor.Current.Value.playerPos[2]);
                Player._Instance.gameObject.transform.position = vector3;
                Player._Instance.animator.SetInteger("DirectionEnum",itor.Current.Value.playerFaceTo);
                return;
            }
        }
        Debug.LogError("未能找到对应的From与Target场景，Playerpos和朝向赋值失败");
    }
}
