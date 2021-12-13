using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmDataSaveLoad
{
    // Start is called before the first frame update
    public void SaveData()
    {
        // 定义存档路径
        string dirpath = Application.persistentDataPath + "/Save";
        //创建存档文件夹
        IOHelper.CreateDirectory(dirpath);
        //定义存档文件路径
        string filename = dirpath + "/GameData.sav";
        //保存数据
        IOHelper.SetData(filename, FarmDataManager._Instance);
    }
    public bool LoadData()
    {   // 定义存档路径
        string dirpath = Application.persistentDataPath + "/Save";
        //Debug.LogError(Application.persistentDataPath);
        //创建存档文件夹
        IOHelper.CreateDirectory(dirpath);
        //定义存档文件路径
        string filename = dirpath + "/GameData.sav";

        bool isFileExists= IOHelper.IsFileExists(filename);
        if (isFileExists)
        {
            //读取数据
            FarmDataManager._Instance = (FarmDataManager)IOHelper.GetData(filename, typeof(FarmDataManager));
            return true;
        }
        else
        {
            Debug.Log("SaveData Not Found");
            return false;
        }

    }
}
