using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class MainData 
{
    public static DataManager dataManager = new DataManager();
    //每增加一条需保存的值都要在MainDataDeliver中添加，并在MainData.LoadData中添加相应加载
    public static int year;
    public static int month;
    public static string name;
    public static int age;
    public static int[,,] itemListArr = new int[3,12,2];
    public static List<Vector3Int> plowedData=new List<Vector3Int>() ; //在某格子是否耕过田 的List，List内都是耕过的田的坐标
    public static List<Vector3Int> wateredData=new List<Vector3Int>();   //在某格子是否浇过水 的List，List内都是浇过的水的坐标
    public static Dictionary<Vector3Int, int[]> cropsData=new Dictionary<Vector3Int, int[]>();  //在某格子是否种过地，种的什么，长了几天  的List，List内都是作物们的坐标与状态
    
    /// <summary>
    /// 往ItemListArr中添加一个物品，成功返TRUE，失败返false
    /// </summary>
    /// <param name="id">拾取对象的ID</param>
    /// <param name="num">拾取对象的数量</param>
    /// <returns></returns>
    public static bool ItemAdd(int id,int num)
    {
        //找是否有相同ID的物品，有就叠加数量
        for (int i = 0;i<itemListArr.GetLength(0);i++){
            for (int k = 0;k<itemListArr.GetLength(0);k++){
                if (itemListArr[i, k, 0] == id)
                {
                    itemListArr[i, k, 1] += num;
                    BackpackController bk = new BackpackController();
                    bk.RefreshItemUI();
                    return true;
                }
            }
        }

        //找ItemListArr是否有空位,有就加进去
        int[] SpareLocation = CheckItemListSpare();
        if (SpareLocation!= null)
        {
            itemListArr[SpareLocation[0], SpareLocation[1], 0] = id;
            itemListArr[SpareLocation[0], SpareLocation[1], 1] = num;
            BackpackController bk = new BackpackController();
            bk.RefreshItemUI();
            return true;
        }

        //即没空位又无相同ID
        return false;
    }
    
    /// <summary>
    /// 寻找ItemListArr中的空位，找到返回一个int【】标记坐标,未找到返回null
    /// </summary>
    /// <returns></returns>
    public static int[] CheckItemListSpare()
    {
        int[] SpareLocation = new int[2];
        for (int i = 0;i<itemListArr.GetLength(0);i++){

            for (int k = 0;k<itemListArr.GetLength(0);k++){
                if (itemListArr[i, k, 0] == 0)
                {
                    SpareLocation[0] = i;
                    SpareLocation[1] = k;
                    return SpareLocation;
                }
            }
        }
        return null;
    }

    
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
            itemListArr = t1.ItemListArr;
            plowedData = t1.plowedData;
            wateredData = t1.wateredData;
            cropsData = t1.cropsData;

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
