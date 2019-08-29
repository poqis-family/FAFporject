using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class MainData 
{
    //每增加一条需保存的值都要在MainDataDeliver中添加，并在MainData.LoadData中添加相应加载
    public static int year;
    public static int month;
    public static string name;
    public static int age;
    public static void SaveData()
    {
        // 定义存档路径
        string dirpath = Application.persistentDataPath + "/Save";
        //创建存档文件夹
        IOHelper.CreateDirectory(dirpath);
        //定义存档文件路径
        string filename = dirpath + "/GameData.sav";
        MainDataDeliver temp = new MainDataDeliver();
        //保存数据
        IOHelper.SetData(filename, temp);
    }
    public static bool LoadData()
    {   // 定义存档路径
        string dirpath = Application.persistentDataPath + "/Save";
        //创建存档文件夹
        IOHelper.CreateDirectory(dirpath);
        //定义存档文件路径
        string filename = dirpath + "/GameData.sav";

        bool isFileExists= IOHelper.IsFileExists(filename);
        if (isFileExists)
        {
            //读取数据
            MainDataDeliver t1 = (MainDataDeliver)IOHelper.GetData(filename, typeof(MainDataDeliver));
            //**每次增加需保存数据都要再次添加相应加载**
            year = t1.year;
            month = t1.month;
            name = t1.name;
            age = t1.age;
            //**每次增加需保存数据都要再次添加相应加载**
            return true;
        }
        else
        {
            Debug.Log("SaveData Not Found");
            return false;
        }

    }
}
