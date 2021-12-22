using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WJExcelDataClass;
using Random = UnityEngine.Random;

public class FarmDataManager
{
    public  DataManager dataManager = new DataManager();
    public MainData mainData = new MainData();
    public static FarmDataManager _Instance;
    public PlayerData playerData = new PlayerData();
    
    public FarmDataManager(){
        _Instance = this;
        dataManager.LoadAll();
    }
    
    /// <summary>
    /// 往ItemListArr中添加一个物品，成功返TRUE，失败返false
    /// </summary>
    /// <param name="id">拾取对象的ID</param>
    /// <param name="num">拾取对象的数量</param>
    /// <returns></returns>
    public bool ItemAdd(int id,int num)
    {
        //找是否有相同ID的物品，有就叠加数量
        for (int i = 0;i<mainData.itemListArr.GetLength(0);i++){
            for (int k = 0;k<mainData.itemListArr.GetLength(1);k++){
                if (mainData.itemListArr[i, k].ID == id)
                {
                    mainData.itemListArr[i, k].Num += num;
                    BackpackController._Instance.RefreshItemUI();
                    return true;
                }
            }
        }

        //找ItemListArr是否有空位,有就加进去
        int[] SpareLocation = CheckItemListSpare();
        if (SpareLocation!= null)
        {
            mainData.itemListArr[SpareLocation[0], SpareLocation[1]].ID = id;
            mainData.itemListArr[SpareLocation[0], SpareLocation[1]].Num = num;
            BackpackData.RefreshItemID();
            BackpackController._Instance.RefreshItemUI();
            Player._Instance.CheckItemLiftable();
            return true;
        }

        //即没空位又无相同ID
        Debug.LogError("即没空位又无相同ID");
        return false;
    }
    /// <summary>
    /// 寻找ItemListArr中的空位，找到返回一个int【】标记坐标,未找到返回null
    /// </summary>
    /// <returns></returns>
    private int[] CheckItemListSpare()
    {
        int[] SpareLocation = new int[2];
        for (int i = 0;i<_Instance.mainData.itemListArr.GetLength(0);i++){
            for (int k = 0;k<_Instance.mainData.itemListArr.GetLength(1);k++){
                if (_Instance.mainData.itemListArr[i, k].ID == 0)
                {
                    SpareLocation[0] = i;
                    SpareLocation[1] = k;
                    return SpareLocation;
                }
            }
        }
        return null;
    }

    public void ItemReduce(int BackpackPage,int BackpackIndex)
    {
        BackpackItemSubData item = FarmDataManager._Instance.mainData.itemListArr[BackpackPage, BackpackIndex];
        item.Num -= 1;
        if (item.Num <= 0)
        {
            item.ID = 0;
        }
        BackpackData.RefreshItemID();
        BackpackController._Instance.RefreshItemUI();
    }







    /// <summary>
    /// 添加地块的浇水信息
    /// </summary>
    /// <param name="pos"></param>
    public void AddWaterPlotData(Vector3Int pos)
    {
        PlotData plotDataTemp;
        if (mainData.plotDataDic.TryGetValue(pos, out plotDataTemp))//如果字典有该坐标的信息，就直接改了。没的话把之前新建的那个复个值，然后加进去
        {
            plotDataTemp.IsWatered = true;
        }
        else
        {
            plotDataTemp = new PlotData();
            plotDataTemp.IsWatered = true;
            mainData.plotDataDic.Add(pos,plotDataTemp);
        }
    }
    /// <summary>
    /// 添加地块的锄地信息
    /// </summary>
    /// <param name="pos"></param>
    public void AddPlowPlotData(Vector3Int pos)
    {
        PlotData plotDataTemp;
        if (mainData.plotDataDic.TryGetValue(pos, out plotDataTemp))//如果字典有该坐标的信息，就直接改了。没的话把之前新建的那个复个值，然后加进去
        {
            plotDataTemp.IsPlowed = true;
        }
        else
        {
            plotDataTemp = new PlotData();
            plotDataTemp.IsPlowed = true;
            mainData.plotDataDic.Add(pos,plotDataTemp);
        }
    }
    /// <summary>
    /// 添加地块的作物信息
    /// </summary>
    /// <param name="pos"></param>
    public void AddSowingPlotData(Vector3Int pos,int cropID,int instanceID)
    {
        PlotData plotDataTemp;
        if (mainData.plotDataDic.TryGetValue(pos, out plotDataTemp))//如果字典有该坐标的信息，就直接改了。没的话把之前新建的plotDataTemp复个值，然后加进去
        { 
            plotDataTemp.cropID = cropID;
            plotDataTemp.cropDays = 0;
            plotDataTemp.CropInstanceID = instanceID;
            plotDataTemp.HasCollider = FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).hasCollider;
        }
        else
        {
            plotDataTemp = new PlotData();
            plotDataTemp.cropID = cropID;
            plotDataTemp.cropDays = 0;
            plotDataTemp.CropInstanceID = instanceID;
            plotDataTemp.HasCollider = FarmDataManager._Instance.dataManager.GetCropsItemByID(cropID).hasCollider;
            
            mainData.plotDataDic.Add(pos,plotDataTemp);
            
        }
    }
    
    public void SaveData()
    {
        // 定义存档路径
        string dirpath = Application.persistentDataPath + "/Save";
        //创建存档文件夹
        IOHelper.CreateDirectory(dirpath);
        //定义存档文件路径
        string filename = dirpath + "/GameData.sav";
        //保存数据
        IOHelper.SetData(filename, _Instance);
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
            _Instance = (FarmDataManager)IOHelper.GetData(filename, typeof(FarmDataManager));
            return true;
        }
        else
        {
            Debug.Log("SaveData Not Found");
            return false;
        }

    }

    public void DayPass()
    {
        int cropstage;
        mainData.days += 1;
        Debug.Log(mainData.days);
        foreach (var plot in mainData.plotDataDic)
        {
            if (plot.Value.IsWatered && plot.Value.cropID != 0)//如果浇过水且种了东西
            {
                cropstage = (int)plot.Value.GetCropStage();
                if ((bool) plot.Value.CheckCropMature() == false)//种的东西的阶段非成熟阶段
                {
                    plot.Value.cropDays += 1;
                }
            }
            plot.Value.IsWatered = false;//清空浇水状态
        }
        TileMapController._Instance.RefreshTilemap();
        ReplenishPlayerHPAndVitality();
        SaveData();
    }

    public void ReplenishPlayerHPAndVitality()
    {
        playerData.nowVitality = playerData.maxVitality;
        playerData.nowHP = playerData.maxHP;
    }

    public bool VitalityConsume(int minCost,int maxCost)
    {
        if (playerData.nowVitality > 0)
        {
            int vitalityCost = Random.Range(minCost, maxCost + 1);
            playerData.nowVitality -= vitalityCost;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void VitalittRegain(int minRegain,int maxRegain)
    {
        int vitalityRegain = Random.Range(minRegain, maxRegain + 1);
        if (playerData.nowVitality+vitalityRegain<=playerData.maxVitality)
        {
            playerData.nowVitality += vitalityRegain;
        }
        else
        {
            playerData.nowVitality = playerData.maxVitality;
        }
    }

    public void DeleteCropData(Vector3Int pos)
    {
        if (mainData.plotDataDic.TryGetValue(pos, out PlotData plot))
        {
            plot.cropID = 0;
            plot.CropInstanceID = 0;
            plot.cropDays = 0;
        }
    }
}
