using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FarmDataManagerLauncher : MonoBehaviour
{

    void Awake()
    {
        FarmDataManager farmDataManager = new FarmDataManager();
        SceneJumper sceneJumper = new SceneJumper();
        for (int i = 0; i < FarmDataManager._Instance.mainData.itemListArr.GetLength(0); i++)
        {
            for (int j = 0; j < FarmDataManager._Instance.mainData.itemListArr.GetLength(1); j++)
            {
                FarmDataManager._Instance.mainData.itemListArr[i,j] = new BackpackItemSubData();
            }
        }
    }
    public void Click()
    {
        SceneJumper._Instance.SceneJump("Farm");
    }
}
