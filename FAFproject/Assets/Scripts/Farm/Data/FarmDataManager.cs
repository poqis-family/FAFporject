using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmDataManager
{
    public  DataManager dataManager = new DataManager();
    public MainData mainData = new MainData();
    public static FarmDataManager _Instance;
    
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
                if (mainData.itemListArr[i, k, 0] == id)
                {
                    mainData.itemListArr[i, k, 1] += num;
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
            mainData.itemListArr[SpareLocation[0], SpareLocation[1], 0] = id;
            mainData.itemListArr[SpareLocation[0], SpareLocation[1], 1] = num;
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
    public int[] CheckItemListSpare()
    {
        int[] SpareLocation = new int[2];
        for (int i = 0;i<_Instance.mainData.itemListArr.GetLength(0);i++){

            for (int k = 0;k<_Instance.mainData.itemListArr.GetLength(0);k++){
                if (_Instance.mainData.itemListArr[i, k, 0] == 0)
                {
                    SpareLocation[0] = i;
                    SpareLocation[1] = k;
                    return SpareLocation;
                }
            }
        }
        return null;
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
            plotDataTemp.isWatered = true;
        }
        else
        {
            plotDataTemp = new PlotData();
            plotDataTemp.isWatered = true;
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
            plotDataTemp.isPlowed = true;
        }
        else
        {
            plotDataTemp = new PlotData();
            plotDataTemp.isPlowed = true;
            mainData.plotDataDic.Add(pos,plotDataTemp);
        }
    }
    /// <summary>
    /// 添加地块的作物信息
    /// </summary>
    /// <param name="pos"></param>
    public void AddSowingPlotData(Vector3Int pos,int cropID)
    {
        PlotData plotDataTemp;
        if (mainData.plotDataDic.TryGetValue(pos, out plotDataTemp))//如果字典有该坐标的信息，就直接改了。没的话把之前新建的那个复个值，然后加进去
        { 
            plotDataTemp.cropID = cropID;
            plotDataTemp.cropDays = 0;
        }
        else
        {
            plotDataTemp = new PlotData();
            plotDataTemp.cropID = cropID;
            plotDataTemp.cropDays = 0;
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
            if (plot.Value.isWatered && plot.Value.cropID != 0)//如果浇过水且种了东西
            {
                cropstage = (int)plot.Value.GetCropStage();
                if ((bool) plot.Value.CheckCropMature(cropstage) == false)//种的东西的阶段非成熟阶段
                {
                    plot.Value.cropDays += 1;
                }
            }
            plot.Value.isWatered = false;//清空浇水状态
        }
        TileMapController._Instance.RefreshTilemap();
        SaveData();
    }
}
